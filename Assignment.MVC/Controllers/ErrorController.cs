using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment.MVC.Controllers
{
    public class ErrorController : Controller
    {
        /// <summary>
        /// Provides View for Not Found URL
        /// </summary>
        /// <returns>View</returns>
        public ActionResult NotFound()
        {
            return View();
        }
    }
}