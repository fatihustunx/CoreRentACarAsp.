using Core.Utilities.Results;
using Entities.Conceretes;
using Entities.DTOs;
using Entities.DTOs.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IRentalService
    {
        IDataResult<List<Rental>> GetAll
            (Expression<Func<Rental,bool>>? filter = null);
        IDataResult<GetRentForAddDto> Add(RentalForAdd rentalForAdd);
        IResult Update(Rental rental);
        IResult Delete(Rental rental);
        IDataResult<Rental> Get(int id);

        IDataResult<List<GetAllRentalDto>> GetAllRentalDtos();

        IDataResult<List<Rental>> GetAllByCarId(int carId);
        IDataResult<List<Rental>> GetAllByCustomerId(int customerId);
    }
}