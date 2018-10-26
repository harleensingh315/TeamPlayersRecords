using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TeamPlayerRecords.Startup))]
namespace TeamPlayerRecords
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
