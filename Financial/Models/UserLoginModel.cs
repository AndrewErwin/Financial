﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Financial.Models
{
    public class UserLoginModel
    {
        [Required]
        [StringLength(20, MinimumLength = 5)]
        public String Login { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6)]
        public String Password { get; set; }

        public String ReturnUrl { get; set; }
    }
}