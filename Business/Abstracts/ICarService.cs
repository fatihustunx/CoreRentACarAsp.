using Core.Utilities.Results;
using Entities.Conceretes;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface ICarService
    {
        IDataResult<List<Car>> GetAll
            (Expression<Func<Car,bool>>? filter = null);
        IResult Add(Car car);
        IResult Update(Car car);
        IResult Delete(Car car);

        IDataResult<Car> Get(int id);
        IDataResult<List<GetAllCarDto>> GetAllCarDtos();
        IDataResult<List<GetAllCarDto>> GetAllWithoutRents();
        IDataResult<List<Car>> GetAllByBrandId(int brandId);
        IDataResult<List<Car>> GetAllByColorId(int colorId);
        IDataResult<CarDetailDto> GetCarDetails(int id);

        IDataResult<List<Car>> GetAllByFilter(int brandId, int colorId);
    }
}