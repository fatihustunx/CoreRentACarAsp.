using Entities.Conceretes;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class CarImageValidator : AbstractValidator<CarImage>
    {
        public CarImageValidator()
        {
            RuleFor(i => i.Id).NotNull().NotEmpty();
            RuleFor(i => i.CarId).NotNull().NotEmpty();
        }
    }
}
