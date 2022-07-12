using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;



[assembly: OwinStartup(typeof(randevu.IdentityConfig))]

namespace randevu
{
    public class IdentityConfig
    {
        public void Configuration(IAppBuilder app)
        {


            app.UseCookieAuthentication(new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Home/Beauty")
            });
        }
    }
}
