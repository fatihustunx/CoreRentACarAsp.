using Business.Abstracts;
using Business.Constants;
using DataAccess.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Rules
{
    public class RentalBusinessRules
    {
        //IRentalDal _rentalDal;
        IRentalService _rentalService;

        //public RentalBusinessRules(IRentalDal rentalDal)
        //{
        //    _rentalDal = rentalDal;
        //}

        public void Set_rentalService(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        public void checkIfCarReturnDateIsNull(int carId)
        {
            var result = _rentalService.GetAllByCarId(carId);

            foreach (var item in result.Data)
            {
                if(item.ReturnDate == null)
                {
                    throw new Exception(Messages.RentalCarIsNotReturn);
                }
            }
        }
    }
}