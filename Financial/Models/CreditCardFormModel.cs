using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Financial.Internationalization.Entities.CreditCard;

namespace Financial.Models
{
    public class CreditCardFormModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [Display(ResourceType = typeof(CreditCardEntityLabels), Name = "Nickname")]
        [StringLength(40, MinimumLength = 5)]
        public String Nickname { get; set; }

        [Required]
        [Display(ResourceType = typeof(CreditCardEntityLabels), Name = "Network")]
        public Guid NetworkId { get; set; }

        [Display(ResourceType = typeof(CreditCardEntityLabels), Name = "International")]
        public Boolean International { get; set; }
    }
}