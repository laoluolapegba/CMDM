using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CMdm.UI.Web.Startup))]
namespace CMdm.UI.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
