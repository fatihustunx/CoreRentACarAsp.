using Entities.Conceretes;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class CarValidatior : AbstractValidator<Car>
    {
        public CarValidatior()
        {
            //RuleFor(c=> c.Id).NotNull().NotEmpty();
            RuleFor(c => c.BrandId).NotNull().NotEmpty();
            RuleFor(c => c.ColorId).NotNull().NotEmpty();

            RuleFor(c => c.Name).NotEmpty().MinimumLength(2);

            RuleFor(c => c.ModelYear).NotNull().NotEmpty();
            RuleFor(c => c.DailyPrice).NotNull().NotEmpty()
                .GreaterThan(0);
        }
    }
}
