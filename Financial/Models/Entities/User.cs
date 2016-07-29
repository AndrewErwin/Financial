using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Financial.Models.Entities
{
    [Table("Users")]
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5)]
        public String Name { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 5)]
        public String Login { get; set; }
    }
}