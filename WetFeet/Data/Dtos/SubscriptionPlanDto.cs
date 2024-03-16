﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos
{
    public class SubscriptionPlanDto
    {
        public Guid Id { get; set; }
        public string? Details { get; set; }
        public double MonthlyAmount { get; set; }
        public double YearlyAmount { get; set; }
        public bool? IsActive { get; set; }
    }
}
