using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignment.MVC.Models;
using Assignment.MVC.Models.DataEntities;

namespace Assignment.MVC.CustomFilter
{
    public class CustomExceptionHandleAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                ExceptionLogger logger = new ExceptionLogger()
                {
                    ExceptionMessage = filterContext.Exception.Message,
                    ExceptionStackTrace = filterContext.Exception.StackTrace,
                    ControllerName = filterContext.RouteData.Values["controller"].ToString(),
                    LogTime = DateTime.Now
                };

                using (var _context = new ApplicationDbContext())
                {
                    _context.ExceptionLoggers.Add(logger);
                    _context.SaveChanges();
                }

                filterContext.ExceptionHandled = false;
            }
        }
    }
}