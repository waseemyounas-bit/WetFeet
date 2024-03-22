using Data.Entities.Common;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.IServices
{
    public interface IPaymentService
    {
        Payment AddPayment(Payment payment);
        int UpdatePayment(Payment payment);
        Payment GetPaymentById(Guid id);
        List<Payment> GetAllPayments(string userId);
        int DeletePayment(Guid id);
        Payment GetPaymentByUserId(string userId);
    }
}
