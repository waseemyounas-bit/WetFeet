using Data.Dtos;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.IServices
{
    public interface ISubscriptionService
    {
        List<SubscriptionPlan> GetAllSubscriptionPlans();
        SubscriptionPlan GetSubscriptionPlan(Guid id);

        int AddUserSubscriptionPlan(UserSubscriptionPlan userSubscriptionPlan);

        List<UserSubscriptionPlan> GetUserSubscriptionPlans(string userId);
    }
}
