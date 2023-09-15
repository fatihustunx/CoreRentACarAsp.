﻿using Entities.Conceretes;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class RentalValidator : AbstractValidator<Rental>
    {
        public RentalValidator()
        {
            RuleFor(r => r.Id).NotNull().NotEmpty();
            RuleFor(r => r.CarId).NotNull().NotEmpty();
            RuleFor(r => r.CustomerId).NotNull().NotEmpty();
            RuleFor(r => r.RentDate).NotNull().NotEmpty();
        }
    }
}
