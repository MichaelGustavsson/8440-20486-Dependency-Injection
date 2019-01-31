using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CourseTrack.Startup))]
namespace CourseTrack
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
