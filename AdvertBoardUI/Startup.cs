using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AdvertBoardUI.Startup))]
namespace AdvertBoardUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
