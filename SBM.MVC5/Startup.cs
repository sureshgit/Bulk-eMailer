using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SBM.MVC5.Startup))]
namespace SBM.MVC5
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
