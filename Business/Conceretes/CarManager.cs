using Business.Abstracts;
using DataAccess.Abstracts;
using Entities.Conceretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Conceretes
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;
        CarBusinessRules _carBusinessRules;

        public CarManager(ICarDal carDal, CarBusinessRules carBusinessRules)
        {
            _carDal = carDal;
            _carBusinessRules = carBusinessRules;
        }

        public void Add(Car car)
        {
            _carBusinessRules.checkIfCarNameIsTrue("CarName");
            _carBusinessRules.checkIfDailyPriceIsTrue(car.DailyPrice);
            _carDal.Add(car);
        }

        public void Delete(Car car)
        {
            _carDal.Delete(car);
        }

        public List<Car> GetAll()
        {
           return _carDal.GetAll();
        }

        public Car Get(int id)
        {
            return _carDal.Get(p=>p.Id == id);
        }

        public void Update(Car car)
        {
            _carDal.Update(car);
        }

        public List<Car> GetAllByBrandId(int brandId)
        {
            return _carDal.GetAll(c => c.BrandId == brandId);
        }

        public List<Car> GetAllByColorId(int colorId)
        {
            return _carDal.GetAll(c => c.ColorId == colorId);
        }
    }
}
