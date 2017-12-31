using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PLCLAB.Startup))]
namespace PLCLAB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
