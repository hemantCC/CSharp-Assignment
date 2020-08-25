using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Assignment.MVC.CustomFilter;

namespace Assignment.MVC.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {

        /// <summary>
        /// Home page of the application
        /// </summary>  
        /// <returns>View</returns>
        [OutputCache(CacheProfile = "CacheProfileClient", Location = OutputCacheLocation.Client, NoStore = true)]
        [CustomExceptionHandle]
        public ActionResult Index()
        {
            return View();
        }
       
    }
}