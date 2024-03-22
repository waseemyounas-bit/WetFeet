using Data.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class AddStripePayment
    {
        public string CustomerId { get; set; } = string.Empty;
        public string ReceiptEmail { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Currency { get; set; } = string.Empty;
        public long Amount { get; set; } = 0;
    }
}
