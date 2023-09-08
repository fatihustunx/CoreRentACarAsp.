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
                throw new ArgumentException("Car name is not valid !");
            }
        }

        public void checkIfDailyPriceIsTrue(decimal dailyPrice)
        {
            if(dailyPrice <= 0) {
                throw new ArgumentException("Car's daily price is not valid !");
            }
        }
    }
}
