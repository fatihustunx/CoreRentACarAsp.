using Business.Abstracts;
using Business.BusinessRules.Abstracts;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.BusinessRules.Conceretes
{
    public class RentalBusinessRules : IRentalBusinessRules
    {
        IRentalDal _rentalDal;

        public RentalBusinessRules(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }

        public IDataResult<GetRentForAddDto> checkIfRentalCarRentDateIsPass(int carId, DateTime rentDay)
        {
            var result = _rentalDal.GetAll(r => r.CarId == carId);

            if (result == null) { return new SuccessDataResult<GetRentForAddDto>(); }

            foreach(var item in result)
            {
                if(rentDay>=item.RentDate && rentDay < item.ReturnDate)
                {
                    return new ErrorDataResult<GetRentForAddDto>(Messages.RentalCarIsPassRentDay);
                }
            }
            return new SuccessDataResult<GetRentForAddDto>();
        }

        public IDataResult<GetRentForAddDto> checkIfRentalCarReturnDateIsPass(int carId, DateTime returnDay)
        {
            var result = _rentalDal.GetAll(r => r.CarId==carId);

            if(result== null) { return new ErrorDataResult<GetRentForAddDto>(); }

            foreach (var item in result)
            {
                if(returnDay>item.RentDate && returnDay <= item.ReturnDate)
                {
                    return new ErrorDataResult<GetRentForAddDto>(Messages.RentalCarIsPassReturnDay);
                }
            }
            return new SuccessDataResult<GetRentForAddDto>();
        }
    }
}