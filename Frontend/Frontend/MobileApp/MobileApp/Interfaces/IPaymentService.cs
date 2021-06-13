using MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Interfaces
{
    public interface IPaymentService
    {
        Task<bool> PayByIDeal(Payment payment);
    }
}
