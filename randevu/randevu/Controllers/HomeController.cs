using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using randevu.Identity;
using randevu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace randevu.Controllers
{
    public class HomeController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        public HomeController()
        {
            var userStore = new UserStore<ApplicationUser>(new IdentityDataContext());
            userManager = new UserManager<ApplicationUser>(userStore);

        }

        [HttpGet]
        public ActionResult Index(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public ActionResult Index(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid && model.Username!="mehtap")
            {
                
                var user = userManager.Find(model.Username, model.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Yanlış kullanıcı adı veya parola");
                }
                else
                {
                    var authManager = HttpContext.GetOwinContext().Authentication;
                    var identity = userManager.CreateIdentity(user, "ApplicationCookie");
                    
                    var authProperties = new AuthenticationProperties()
                    {
                        IsPersistent = true,

                    };
                    authManager.SignOut();
                    authManager.SignIn(authProperties, identity);
               
                    return Redirect(string.IsNullOrEmpty(returnUrl) ? "/" : returnUrl);
                }
            }

            ViewBag.returnUrl = returnUrl;
            return View(model);
        }

        [Authorize]
        public ActionResult Logout()
        {

            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();
            return RedirectToAction("Index");
        }


        public ActionResult Beauty()
        {
            return View();
        }

        [Authorize]
        public ActionResult Filter()
        {
            return View();
        }



        public ActionResult Contact()
        {
            return View();
        }


    }
}