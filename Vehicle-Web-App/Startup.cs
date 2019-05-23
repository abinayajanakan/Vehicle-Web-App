using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Vehicle_Web_App.Startup))]
namespace Vehicle_Web_App
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
