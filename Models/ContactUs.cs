using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeastLords.Models
{
    public class ContactUs
    {

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

      
        public string Name { get; set; }

        [Required]
        public string Subject { get; set; }

        [StringLength(100, MinimumLength = 5,ErrorMessage="Need at least {2} characters")]
        [Required]
        public string Message { get; set; }
  

    }
}