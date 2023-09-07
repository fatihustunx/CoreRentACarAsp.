using DataAccess.Abstracts;
using Entities.Conceretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Conceretes.InMemory
{
    public class InMemoryCarDal : ICarDal
    {
        List<Car> _cars;

        public InMemoryCarDal()
        {
            _cars = new List<Car> { new Car { Id = 1,BrandId = 1,ColorId = 1,ModelYear = 2001,DailyPrice=200,Description ="good a car" },
            new Car { Id = 2,BrandId = 1,ColorId = 1,ModelYear = 2002,DailyPrice=300,Description ="nice a car" },
            new Car { Id = 3,BrandId = 2,ColorId = 2,ModelYear = 2002,DailyPrice=300,Description ="relax a car" },
            new Car { Id = 4,BrandId = 2,ColorId = 2,ModelYear = 2003,DailyPrice=400,Description ="handsome a car" },
            new Car { Id = 5,BrandId = 2,ColorId = 3,ModelYear = 2005,DailyPrice=700,Description ="beatiful a car" }};
        }

        public void Add(Car car)
        {
            _cars.Add(car);
        }

        public void Delete(Car car)
        {
            Car carToDelete = _cars.SingleOrDefault(c => c.Id == car.Id);
            _cars?.Remove(carToDelete);
        }

        public List<Car> GetAll()
        {
            return _cars;
        }

        public Car GetById(int id)
        {
            return _cars.SingleOrDefault(c => c.Id == id);
        }

        public void Update(Car car)
        {
            Car carToUpdate = _cars.SingleOrDefault(c => c.Id == car.Id);
            carToUpdate.ColorId = car.ColorId;
            carToUpdate.DailyPrice = car.DailyPrice;
            carToUpdate.ModelYear = car.ModelYear;
            carToUpdate.Description = car.Description;
        }
    }
}
