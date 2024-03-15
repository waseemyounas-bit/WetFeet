using Business.IServices;
using Data.Context;
using Data.Entities;
using DataAccess.Repository;
using DataAccess.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Data.WetFeetEnums;

namespace Business.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly IRepository<SubscriptionPlan> _repository;
        private readonly IRepository<UserSubscriptionPlan> _userPlanRepository;
        private readonly IUnitofWork _unitofWork;

        public SubscriptionService(IRepository<SubscriptionPlan> repository,
            IRepository<UserSubscriptionPlan> userPlanRepository,
            IUnitofWork unitofWork)
        {
            this._repository = repository;
            this._userPlanRepository = userPlanRepository;
            this._unitofWork = unitofWork;
        }

        public int AddUserSubscriptionPlan(UserSubscriptionPlan userSubscriptionPlan)
        {
            this._userPlanRepository.Add(userSubscriptionPlan);
            return this._unitofWork.saveChanges();
        }

        public List<SubscriptionPlan> GetAllSubscriptionPlans()
        {
            return this._repository.GetDataFiltered(x => x.IsActive == true);
        }

        public SubscriptionPlan GetSubscriptionPlan(Guid id)
        {
            return this._repository.GetById(id);
        }

        public List<UserSubscriptionPlan> GetUserSubscriptionPlans(string userId)
        {
            return this._userPlanRepository.GetDataFiltered(x => x.UserId == userId && x.IsActive == true);
        }
    }
}
