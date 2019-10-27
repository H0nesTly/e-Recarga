using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(e_Recarga.Startup))]
namespace e_Recarga
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
