using Entities.Conceretes;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface ICarService
    {
        List<Car> GetAll();
        void Add(Car car);
        void Update(Car car);
        void Delete(Car car);

        Car Get(int id);
        List<Car> GetAllByBrandId(int brandId);
        List<Car> GetAllByColorId(int colorId);
        List<CarDetailDto> GetCarDetails();
    }
}
