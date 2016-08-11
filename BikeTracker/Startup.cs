using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BikeTracker.Startup))]
namespace BikeTracker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
