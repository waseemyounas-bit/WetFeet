using Data.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Data.Entities
{
    public class StripePayment 
    {
        public string CustomerId { get; set; } = string.Empty;
        public string ReceiptEmail { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Currency { get; set; } = "$";
        public long Amount { get; set; } = 0;
        public string PaymentId { get; set; } = string.Empty;
    }
}
