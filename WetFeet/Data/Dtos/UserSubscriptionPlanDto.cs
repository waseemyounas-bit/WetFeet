using Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Data.WetFeetEnums;

namespace Data.Dtos
{
    public class UserSubscriptionPlanDto
    {
        public Guid Id { get; set; }
        public double Amount { get; set; }
        public SubscriptionType Type { get; set; }
        public bool? IsActive { get; set; }
        public DateTime ActivatedDate { get; set; }
        public string? UserId { get; set; }
        public DateTime RenewalDate { get; set; }
    }
}
