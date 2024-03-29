﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace randevu.Models
{
    public class LoginModel
    {
 
        [Required]
        public string Username { get; set; }


        [Required]
        public string Password { get; set; }


    }



    public class Register
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string  Email { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string NameSurname { get; set; }
    }

    public class RandevuAl
    {
        [Required]
        public string Randevu { get; set; }

        
        public string Time { get; set; }

        [Required]
        public string Text { get; set; }


    }

    public class AdminModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
