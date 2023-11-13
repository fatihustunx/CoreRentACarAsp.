using Business.Abstracts;
using Core.Utilities.Results;
using Entities.DTOs.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Conceretes
{
    public class PayOfRentManager : IRentPaymentService
    {
        private readonly IRentalService _rentalService;
        private readonly IPaymentService _paymentService;

        public PayOfRentManager(IRentalService rentalService, IPaymentService paymentService)
        {
            _rentalService = rentalService;
            _paymentService = paymentService;
        }

        public IResult PayOfRent(PayOfRent payOfRent)
        {
            var rent = _rentalService.Get(payOfRent.RentId);

            if(rent == null)
            {
                return new ErrorResult();
            }

            PaymentForPay paymentForPay = new()
            {
                FullName = payOfRent.FullName,
                CardNo = payOfRent.CardNo,
                Expiration = payOfRent.Expiration,
                CvcCode = payOfRent.CvcCode,
                TotalCost = rent.Data.TotalCost
            };

            if (_paymentService.Pay(paymentForPay).Success)
            {
                rent.Data.IsState = true;

                _rentalService.Update(rent.Data);

                return new SuccessResult();
            }

            return new ErrorResult();
        }
    }
}