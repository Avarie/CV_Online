using cv;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace cv
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
