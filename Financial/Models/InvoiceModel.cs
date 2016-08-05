using System;
using System.Collections.Generic;

namespace Financial.Models
{
    public class InvoiceModel
    {
        public DateTime Begin { get; set; }

        public DateTime End { get; set; }

        public List<InvoiceDetailModel> Details { get; set; }
    }
}