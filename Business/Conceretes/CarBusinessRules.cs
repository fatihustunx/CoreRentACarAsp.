using Business.Constants;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Conceretes
{
    public class CarBusinessRules
    {
        public void checkIfCarNameIsTrue(string carName)
        {
            if(carName.Length < 2)
            {
                throw new ArgumentException(Messages.CarNameIsNotValid);
            }
        }

        public void checkIfDailyPriceIsTrue(decimal dailyPrice)
        {
            if(dailyPrice <= 0) {
                throw new ArgumentException(Messages.CarDailyPriceIsNotValid);
            }
        }
    }
}
