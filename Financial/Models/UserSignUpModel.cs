﻿using System;
using System.ComponentModel.DataAnnotations;
using Financial.Internationalization.Entities.User;

namespace Financial.Models
{
    public class UserSignUpModel
    {
        [Required]
        [StringLength(100, MinimumLength = 5)]
        [Display(ResourceType = typeof(UserEntityLabels), Name = "Name")]
        public String Name { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 5)]
        [Display(ResourceType=typeof(UserEntityLabels), Name="Login")]
        public String Login { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6)]
        [Display(ResourceType = typeof(UserEntityLabels), Name = "Password")]
        public String Password { get; set; }

        [Compare("Password")]
        [Display(ResourceType = typeof(UserEntityLabels), Name = "PasswordConfirm")]
        public String ConfirmPassword { get; set; }
    }
}