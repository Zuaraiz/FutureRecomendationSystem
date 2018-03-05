using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using MultipartDataMediaFormatter;
using MultipartDataMediaFormatter.Infrastructure;
using Microsoft.Owin.Cors;

[assembly: OwinStartup(typeof(Server.Startup1))]

namespace Server
{
    public class Startup1
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
          
           
        }
    }
}
