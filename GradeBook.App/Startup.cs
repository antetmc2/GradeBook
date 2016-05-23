using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GradeBook.App.Startup))]
namespace GradeBook.App
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
