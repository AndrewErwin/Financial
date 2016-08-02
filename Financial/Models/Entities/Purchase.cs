using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Financial.Internationalization.Entities.Purchase;

namespace Financial.Models.Entities
{
    [Table("Purchases")]
    public class Purchase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [Display(ResourceType = typeof(PurchaseEntityLabels), Name = "Field_Description")]
        [StringLength(20, MinimumLength = 5)]
        public String Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(ResourceType = typeof(PurchaseEntityLabels), Name = "Field_PurchasedOn")]
        public DateTime PurchasedOn { get; set; }

        [Required]
        [Display(ResourceType = typeof(PurchaseEntityLabels), Name = "Field_InstalmentSplit")]
        public int InstalmentSplit { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(ResourceType = typeof(PurchaseEntityLabels), Name = "Field_TotalAmount")]
        public Decimal TotalAmount { get; set; }

        [Display(ResourceType = typeof(PurchaseEntityLabels), Name = "Field_CreditCard")]
        public Guid? CreditCardId { get; set; }
        public virtual CreditCard CreditCard { get; set; }

        [Required]
        public int OwnerId { get; set; }
        public virtual User Owner { get; set; }
    }
}