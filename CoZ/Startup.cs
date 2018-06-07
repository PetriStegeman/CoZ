using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CoZ.Startup))]
namespace CoZ
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
