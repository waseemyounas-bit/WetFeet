using Data.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Payment : BaseEntity
    {
        public double PaidAmount { get; set; }
        [ForeignKey(nameof(ApplicationUser))]
        public string? UserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
        public string? StripePaymentId { get; set;}
        public string? PaymentReceipt { get; set;}
        public bool? IsPaymentReleased { get; set; } = false;
        public bool? IsTransferred { get; set; }=false;
        public DateTime? TransferDate { get; set; }

        [ForeignKey(nameof(UserSubscriptionPlan))]
        public Guid? UserSubscriptionId { get; set; }
        public UserSubscriptionPlan? UserSubscriptionPlan { get; set; }
    }
}
