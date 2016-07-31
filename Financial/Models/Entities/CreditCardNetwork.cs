using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Financial.Internationalization.Entities.CreditCardNetwork;

namespace Financial.Models.Entities
{
    [Table("CreditCardNetworks")]
    public class CreditCardNetwork
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [Display(ResourceType = typeof(CCNetworkEntityLabels), Name = "DisplayName")]
        public String DisplayName { get; set; }

        [Required]
        [Display(ResourceType = typeof(CCNetworkEntityLabels), Name = "ImageName")]
        public String ImageName { get; set; }
    }
}