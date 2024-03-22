using Business.IServices;
using Data.Entities;
using DataAccess.Repository;
using DataAccess.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IRepository<Payment> repository;
        private readonly IUnitofWork unitofWork;

        public PaymentService(IRepository<Payment> _repository, IUnitofWork _unitofWork)
        {
            repository = _repository;
            unitofWork = _unitofWork;
        }
        public Payment AddPayment(Payment payment)
        {
            repository.Add(payment);
            unitofWork.saveChanges();
            return payment;
        }

        public int DeletePayment(Guid id)
        {
            repository.Delete(id);
            return unitofWork.saveChanges();
        }

        public List<Payment> GetAllPayments(string userId)
        {
            return repository.GetAll().Where(x => x.UserId == userId).ToList();
        }

        public Payment GetPaymentByUserId(string userId)
        {
            var payment = repository.GetAll().Where(x => x.UserId == userId).FirstOrDefault();
            return payment ?? null;
        }

        public Payment GetPaymentById(Guid id)
        {
            return repository.GetById(id);
        }

        public int UpdatePayment(Payment payment)
        {
            repository.Update(payment);
            return this.unitofWork.saveChanges();
        }
    }
}
