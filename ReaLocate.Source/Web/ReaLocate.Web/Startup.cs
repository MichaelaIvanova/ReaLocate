using Microsoft.Owin;

using Owin;

[assembly: OwinStartupAttribute(typeof(ReaLocate.Web.Startup))]

namespace ReaLocate.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            this.ConfigureAuth(app);
        }
    }
}
