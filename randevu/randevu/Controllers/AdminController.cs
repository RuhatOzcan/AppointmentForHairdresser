using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using randevu.Identity;
using randevu.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace randevu.Controllers
{
    public class AdminController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        public AdminController()
        {
            var userStore = new UserStore<ApplicationUser>(new IdentityDataContext());
            userManager = new UserManager<ApplicationUser>(userStore);
            
        }



        [HttpGet]
        public ActionResult AdminLogin(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }
        


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminLogin(AdminModel model, string returnUrl)
        {
            {
                if (ModelState.IsValid)
                {
                 
                    var user = userManager.Find(model.Username, model.Password);
                    if (user == null)
                    {
                        ModelState.AddModelError("", "Yanlış kullanıcı adı veya parola");
                    }
                    else
                    {
                        if (model.Username=="mehtap")
                        {
                            var authManager = HttpContext.GetOwinContext().Authentication;
                            var identity = userManager.CreateIdentity(user, "ApplicationCookie");

                            var authProperties = new AuthenticationProperties()
                            {
                                IsPersistent = true,

                            };
                            authManager.SignOut();
                            authManager.SignIn(authProperties, identity);

                            return Redirect(string.IsNullOrEmpty(returnUrl) ? "/Admin/Mehtap" : returnUrl);
                        }
                        else
                        {
                            ModelState.AddModelError("", "Buradan Sadece Kuaforler giriş yapabilir");
                        }
                        
                    }
                }

                ViewBag.returnUrl = returnUrl;
                return View(model);
            }
        }




        [Authorize]
        public ActionResult Mehtap()
        { 
            
            return View(userManager.Users);

        }

    }
}