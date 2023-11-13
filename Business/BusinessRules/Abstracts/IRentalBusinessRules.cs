using Core.Utilities.Results;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.BusinessRules.Abstracts
{
    public interface IRentalBusinessRules
    {
        public IDataResult<GetRentForAddDto> checkIfRentalCarRentDateIsPass(int carId, DateTime rentDay);

        public IDataResult<GetRentForAddDto> checkIfRentalCarReturnDateIsPass(int carId, DateTime returnDay);
    }
}