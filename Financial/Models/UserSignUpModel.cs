using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Financial.Models
{
    public class UserSignUpModel
    {
        [Required]
        [StringLength(100, MinimumLength = 5)]
        public String Name { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 5)]
        public String Login { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6)]
        public String Password { get; set; }

        [Compare("Password")]
        public String ConfirmPassword { get; set; }
    }
}