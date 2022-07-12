using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace randevu.Identity
{
    public class ApplicationUser:IdentityUser
    {
   
        public string Randevu { get; set; }

        public string Text { get; set; }
        
        public string Salon { get; set; }

        public string Saat { get; set; }

        public string NameSurname { get; set; }


    }
}