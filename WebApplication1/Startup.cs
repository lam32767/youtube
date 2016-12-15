using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyYouTube.Startup))]
namespace MyYouTube
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
