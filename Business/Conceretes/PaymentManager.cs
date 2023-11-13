using Business.Abstracts;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using Entities.DTOs.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Conceretes
{
    public class PaymentManager : IPaymentService
    {
        private readonly IPaymentDal _paymentDal;

        public PaymentManager(IPaymentDal paymentDal)
        {
            _paymentDal = paymentDal;
        }

        public IResult Pay(PaymentForPay paymentForPay)
        {
            var payment = _paymentDal.Get
                (p => p.CardNo.Equals(paymentForPay.CardNo));

            if (payment == null)
            {
                return new ErrorResult();
            }

            if(!payment.FullName.Equals(paymentForPay.FullName))
            {
                return new ErrorResult();
            }

            if(payment.Expiration != paymentForPay.Expiration)
            {
                return new ErrorResult();
            }

            if(payment.CvcCode != paymentForPay.CvcCode)
            {
                return new ErrorResult();
            }

            if(payment.Balance>=paymentForPay.TotalCost)
            {
                payment.Balance = 
                    payment.Balance - paymentForPay.TotalCost;

                _paymentDal.Update(payment);
                return new SuccessResult();
            }
            else
            {
                return new ErrorResult();
            }
        }
    }
}