using System;
using System.ComponentModel.DataAnnotations;
using Financial.Internationalization.Entities.Purchase;

namespace Financial.Models
{
    public class PurchaseFormModel
    {
        public Guid Id { get; set; }

        [Required]
        [Display(ResourceType = typeof(PurchaseEntityLabels), Name = "Field_Description")]
        [StringLength(50, MinimumLength = 5)]
        public String Description { get; set; }

        [Required]
        [Display(ResourceType = typeof(PurchaseEntityLabels), Name = "Field_PurchasedOn")]
        public DateTime PurchasedOn { get; set; }

        [Required]
        [Display(ResourceType = typeof(PurchaseEntityLabels), Name = "Field_InstalmentSplit")]
        public int InstalmentSplit { get; set; }

        [Required]
        [Display(ResourceType = typeof(PurchaseEntityLabels), Name = "Field_TotalAmount")]
        public Decimal TotalAmount { get; set; }

        [Display(ResourceType = typeof(PurchaseEntityLabels), Name = "Field_CreditCard")]
        public Guid? CreditCardId { get; set; }
    }
}