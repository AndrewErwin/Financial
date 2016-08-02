using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Financial.Internationalization.Entities.CreditCard;

namespace Financial.Models.Entities
{
    [Table("CreditCards")]
    public class CreditCard
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
        public virtual CreditCardNetwork Network { get; set; }

        [Required]
        [Display(ResourceType = typeof(CreditCardEntityLabels), Name = "International")]
        public Boolean International { get; set; }

        [Required]
        public int OwnerId { get; set; }
        public virtual User Owner { get; set; }
    }
}