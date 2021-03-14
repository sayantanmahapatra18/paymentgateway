using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PayUMoneyTry.Startup))]
namespace PayUMoneyTry
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
