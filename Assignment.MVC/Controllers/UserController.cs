using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Assignment.MVC.CustomFilter;
using Assignment.MVC.Models;
using Assignment.MVC.Models.BusinessEntities;
using Assignment.MVC.Models.DataEntities;
using AutoMapper;

namespace Assignment.MVC.Controllers
{
    [Authorize]
    [CustomExceptionHandle]
    public class UserController : Controller
    {

        ///// <summary>
        ///// shows profile of current user
        ///// </summary>
        ///// <returns></returns>
        public ActionResult UserProfile()
        {
            return View();
        }


        /// <summary>
        /// Edits User Details
        /// </summary>
        /// <param name="userVM"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditProfile(ProfileVM userVM)
        {
            if (ModelState.IsValid)
            {
                UserProfile UserDomain = Mapper.Map<ProfileVM, UserProfile>(userVM);
                using (var _context = new ApplicationDbContext())
                {
                    bool ValidUser = await _context.UserProfiles.Where(x => x.Id == UserDomain.Id).AnyAsync();
                    if (ValidUser)
                    {
                        _context.Entry(UserDomain).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                    }
                    else
                    { 
                        return Json("fail", JsonRequestBehavior.AllowGet);
                    }
                }
                return Json("success",JsonRequestBehavior.AllowGet);
            }
            else
            {
                ModelState.AddModelError("", "Model State Invalid!");
                return View(userVM);
            }
        }

        //returns Current User
        public async Task<JsonResult> GetCurrentUser()
        {
            using (var _context = new ApplicationDbContext())
            {
                UserProfile profileDomain = await _context.UserProfiles.Where(x => x.Email == User.Identity.Name).FirstOrDefaultAsync();
                ProfileVM UserDTO = new ProfileVM();

                //Mapping Domain to DTO
                UserDTO = Mapper.Map<UserProfile, ProfileVM>(profileDomain);
                return Json(UserDTO,JsonRequestBehavior.AllowGet);
            }
        }

    }
}