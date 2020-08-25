using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Assignment.MVC.Startup))]
namespace Assignment.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
        }
    }
}
