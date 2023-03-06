using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MasterPeicefinal.Startup))]
namespace MasterPeicefinal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
