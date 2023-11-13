using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Constants;
using Entities.Conceretes;
using Entities.DTOs.Requests;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class RentalForAddValidator : AbstractValidator<RentalForAdd>
    {
        public RentalForAddValidator()
        {
            //RuleFor(r => r.Id).NotNull().NotEmpty();
            RuleFor(r => r.CarId).NotNull().NotEmpty();
            RuleFor(r => r.CustomerId).NotNull().NotEmpty();
            RuleFor(r => r.RentDate).NotNull().NotEmpty();
            RuleFor(r => r.ReturnDate).NotNull().NotEmpty();

            RuleFor(r => r.RentDate).Must(Operations.IsParser)
                .WithMessage(Messages.RentalDayIsNotParsing).DependentRules(() =>
                {
                    RuleFor(r => Operations.Parser(r.RentDate)).Must(NotBeforeNow)
                .WithMessage(Messages.RentalDayIsNotBeforeNow);
                });

            RuleFor(r => r.ReturnDate).Must(Operations.IsParser)
                .WithMessage(Messages.RentalDayIsNotParsing).DependentRules(() =>
                {
                    RuleFor(r => r).Must((r, _) => NotBeforeRentalDay
                    (Operations.Parser(r.RentDate), Operations.Parser(r.ReturnDate)))
                .WithMessage(Messages.ReturnDayIsNotBeforeRentalDay);
                });
        }

        private bool NotBeforeRentalDay(DateTime rentDay, DateTime returnDate)
        {
            return returnDate > rentDay;
        }

        private bool NotBeforeNow(DateTime rentDay)
        {
            return rentDay >= DateTime.Now;
        }

    }
}