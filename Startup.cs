using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FPP.Startup))]
namespace FPP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        { 
            ConfigureAuth(app);
        }
    }
}
