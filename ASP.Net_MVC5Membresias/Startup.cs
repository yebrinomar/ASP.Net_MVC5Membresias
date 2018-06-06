using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ASP.Net_MVC5Membresias.Startup))]
namespace ASP.Net_MVC5Membresias
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
