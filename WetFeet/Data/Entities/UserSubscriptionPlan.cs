using Data.Entities.Common;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Data.WetFeetEnums;

namespace Data.Entities
{
    public class UserSubscriptionPlan : BaseEntity
    {
        public double Amount { get; set; }
        public SubscriptionType Type { get; set; }
        public bool? IsActive { get; set; }
        public DateTime ActivatedDate { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public string? UserId { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; }
        [ForeignKey(nameof(SubscriptionPlan))]
        public Guid SubscriptionId { get; set; }
        public virtual SubscriptionPlan? SubscriptionPlan { get; set; }

        [NotMapped]
        public DateTime RenewalDate
        {
            get
            {
                if(Type == SubscriptionType.Monthly)
                    return ActivatedDate.AddMonths(1);
                else
                    return ActivatedDate.AddYears(1);
            }
        }
    }
}
