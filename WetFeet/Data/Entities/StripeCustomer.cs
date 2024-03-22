using Data.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class StripeCustomer
    {
        public string Name { get;set; }=string.Empty;
        public string Email { get;set; } = string.Empty;
        public string CustomerId { get;set; } = string.Empty;
    }
}
