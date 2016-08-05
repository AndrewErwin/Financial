﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Financial.Internationalization.Entities.Subscription;

namespace Financial.Models.Entities
{
    [Table("Subscriptions")]
    public class Subscription
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 5)]
        [Display(ResourceType = typeof(SubscriptionEntityLabels), Name = "Field_Description")]
        public String Description { get; set; }

        [Required]
        [Display(ResourceType = typeof(SubscriptionEntityLabels), Name = "Field_SignatureDate")]
        public DateTime SignatureDate { get; set; }

        [Display(ResourceType = typeof(SubscriptionEntityLabels), Name = "Field_CancellationDate")]
        public DateTime? CancellationDate { get; set; }

        [Required]
        [Display(ResourceType = typeof(SubscriptionEntityLabels), Name = "Field_Price")]
        public Decimal Price { get; set; }

        [Display(ResourceType = typeof(SubscriptionEntityLabels), Name = "Field_CreditCard")]
        public Guid? CreditCardId { get; set; }
        public virtual CreditCard CreditCard { get; set; }

        [Required]
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}