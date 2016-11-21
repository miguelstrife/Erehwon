using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ErehwonMvc.Startup))]
namespace ErehwonMvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
