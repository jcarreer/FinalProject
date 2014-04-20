using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SkyrimShop.Startup))]
namespace SkyrimShop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
