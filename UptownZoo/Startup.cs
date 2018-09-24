using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UptownZoo.Startup))]
namespace UptownZoo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
