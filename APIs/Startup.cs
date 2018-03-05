using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

[assembly: OwinStartup(typeof(APIs.Startup))]
namespace APIs
{
    public class Startup
    {
        
            public void Configuration(IAppBuilder app)
            {
                app.UseCors(CorsOptions.AllowAll);



                GlobalConfiguration.Configure(WebApiConfig.Register);

                GlobalConfiguration.Configuration.EnsureInitialized();

                
            }
        }
}