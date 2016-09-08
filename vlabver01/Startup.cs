using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(vlabver01.Startup))]
namespace vlabver01
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
