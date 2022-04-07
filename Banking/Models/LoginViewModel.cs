using System;
using System.ComponentModel.DataAnnotations;

namespace Banking.Models
{
  

    
        public class LoginViewModel
        {


            [Required(ErrorMessage = "AccountNumber is required.")]
      
            [Display(Name = "AccountNumber")]
            public long AccNumber { get; set; }
            [Required(ErrorMessage = "password is required.")]
            [Display(Name = "Password")]
            [RegularExpression(@"[a-zA-z0-9]{4,10}", ErrorMessage = "Password format is wrong")]
            [DataType(DataType.Password)]
            public string Password { get; set; }
            [Display(Name = "Remember me next time")]
            public bool RememberMe { get; set; }


        }
        public class User
        {
            public int Id { get; set; }
            public long AccNumber { get; set; }
            public string Password { get; set; }
            public string FirstName { get; set; }
            public string Status { get; set; }
            




        }
    }
