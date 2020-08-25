using System.Web;
using System.Web.Mvc;
using Assignment.MVC.CustomFilter;

namespace Assignment.MVC
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new CustomExceptionHandleAttribute());
        }
    }
}
