using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using randevu.Identity;
using randevu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace randevu.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> userManager;





        public AccountController()
        {
            var userStore = new UserStore<ApplicationUser>(new IdentityDataContext());
            userManager = new UserManager<ApplicationUser>(userStore);

            userManager.PasswordValidator = new CustomPasswordValidator()
            {
                RequireDigit = true,
                RequiredLength=7,
                RequireLowercase = true,
                RequireUppercase = true
            };
            userManager.UserValidator = new UserValidator<ApplicationUser>(userManager)
            {
                RequireUniqueEmail = true,
                AllowOnlyAlphanumericUserNames = false,
            };

        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Register model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser();
                user.UserName = model.Username;
                user.Email = model.Email;
                user.PhoneNumber = model.Phone;
                user.NameSurname = model.NameSurname;



                var result = userManager.Create(user, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("../Home/Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

            }
            return View(model);
        }






        string sackesim = null;
        string fon = null;
        string agda = null;
        string kasalım = null;
        bool checkbox1 = false;
        bool checkbox2 = false;
        bool checkbox3 = false;
        bool checkbox4 = false;

        




        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult RandevuAl(RandevuAl model, string SacKesim, string Fon, string Agda, string KasAlım)
        {
            IdentityDataContext db = new IdentityDataContext();

            var users = db.Users.ToList();

            ViewBag.Kullanicilar = users;

         

            checkbox1 = false;
            checkbox2 = false;
            checkbox3 = false;
            checkbox4 = false;
            if (SacKesim == "true")
            {
                sackesim = " SaçKesim ";
                checkbox1 = true;

            }
            if (Fon == "true")
            {
                fon = "  Fön  ";
                checkbox2 = true;
            }
            if (Agda == "true")
            {
                agda = "  Ağda  ";
                checkbox3 = true;
            }
            if (KasAlım == "true")
            {
                kasalım = "  KaşAlma  ";
                checkbox4 = true;
            }

            if (checkbox1 == true || checkbox2 == true || checkbox3 == true || checkbox4 == true)
            {


                foreach (var user in users.Where(i => i.Id == User.Identity.GetUserId()))
                {
                    user.Saat = model.Time;
                    user.Randevu = sackesim + fon + agda + kasalım;
                    user.Text = model.Text;
                    user.Salon = "Mehtap";
                }
                db.SaveChanges();

                return View();
            }
            else
            {
                return Redirect("/Account/RandevuAl");
            }
        }

        [HttpGet]
        [Authorize]
        public ActionResult RandevuAl()
        {
            IdentityDataContext db = new IdentityDataContext();
            var users = db.Users.ToList();

            ViewBag.Kullanicilar = users;


            return View();
        }



        [Authorize]
        public ActionResult Delete()
        {
            IdentityDataContext db = new IdentityDataContext();

            var users = db.Users.ToList();

            foreach (var user in users.Where(i => i.Id == User.Identity.GetUserId()))
                {
                    user.Saat = null;
                    user.Randevu = null;
                    user.Text = null;
                    user.Salon = null;
                }
                db.SaveChanges();
            
            return RedirectToAction("/RandevuAl");
        }





    }

}
