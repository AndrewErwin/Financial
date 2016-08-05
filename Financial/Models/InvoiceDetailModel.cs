using System;
using System.ComponentModel.DataAnnotations;
using Financial.Internationalization.Entities.CreditCard;

namespace Financial.Models
{
    public class InvoiceDetailModel
    {
        public Guid Id { get; set; }

        public String Icon { get; set; }

        public String RouteToEntity { get; set; }

        [Display(ResourceType = typeof(CreditCardEntityLabels), Name = "InvoiceDetail_Day")]
        public DateTime Date { get; set; }

        [Display(ResourceType = typeof(CreditCardEntityLabels), Name = "InvoiceDetail_Description")]
        public String Description { get; set; }

        public int AtualSplit { get; set; }

        public int Splits { get; set; }

        [Display(ResourceType = typeof(CreditCardEntityLabels), Name = "InvoiceDetail_Value")]        
        public Decimal Value { get; set; }
    }
}