using Business.Abstracts;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.BusinessRules
{
    public class RentalBusinessRules : IRentalBusinessRules
    {
        IRentalDal _rentalDal;

        public RentalBusinessRules(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }

        public IResult checkIfRentalCarReturnDateIsNull(int carId)
        {
            var result = _rentalDal.GetAll(r => r.CarId == carId);

            foreach (var item in result)
            {
                if (item.ReturnDate == null)
                {
                    return new ErrorResult(Messages.RentalCarIsNotReturn);
                }
            }
            return new SuccessResult();
        }
    }
}
