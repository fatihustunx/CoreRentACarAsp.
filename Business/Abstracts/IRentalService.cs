using Core.Utilities.Results;
using Entities.Conceretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IRentalService
    {
        IDataResult<List<IResult>> Add(Rental rental);
        IDataResult<List<Rental>> GetAll();
        IResult Update(Rental rental);
        IResult Delete(Rental rental);
        IDataResult<Rental> Get(int id);

        IDataResult<List<Rental>> GetAllByCarId(int carId);
        IDataResult<List<Rental>> GetAllByCustomerId(int customerId);
    }
}