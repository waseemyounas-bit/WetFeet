using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class AddStripeCard
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string CardNumber { get; set; } = string.Empty;
        [Required]
        public string ExpirationYear { get; set; } = string.Empty;
        [Required]
        public string ExpirationMonth { get; set; } = string.Empty;
        [Required]
        public string Cvv { get; set; } = string.Empty;
        public string? UserId { get; set; }
        public double Amount { get; set; } = 0;
        public Guid? UserSubscriptionId { get; set; }
    }
}
