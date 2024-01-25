using AutoMapper;
using Business.Abstracts;
using Business.BusinessAspects.Autofac.Security;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using Entities.Conceretes;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Conceretes
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;

        Lazy<IRentalService> _circular;

        //ServiceLocator
        //IServiceProvider

        public CarManager(ICarDal carDal,
            Lazy<IRentalService> circular)
        {
            _carDal = carDal;
            _circular = circular;
        }

        [SecuredOperation("Admin,car.add")]
        [CacheRemoveAspect("ICarService.Get")]
        [ValidationAspect(typeof(CarValidatior))]
        public IResult Add(Car car)
        {
            _carDal.Add(car);

            return new SuccessResult();
        }

        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult();
        }

        [CacheAspect]
        public IDataResult<List<GetAllCarDto>> GetAllCarDtos()
        {
            return new SuccessDataResult<List<GetAllCarDto>>(_carDal.GetAllCarDtos());
        }

        [CacheAspect]
        public IDataResult<Car> Get(int id)
        {
            return new SuccessDataResult<Car>(_carDal.Get(c=>c.Id == id));
        }

        public IResult Update(Car car)
        {
            _carDal.Update(car);
            return new SuccessResult();
        }

        public IDataResult<List<Car>> GetAllByBrandId(int brandId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.BrandId == brandId));
        }

        public IDataResult<List<Car>> GetAllByColorId(int colorId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.ColorId == colorId));
        }

        public IDataResult<CarDetailDto> GetCarDetails(int id)
        {
            return new SuccessDataResult<CarDetailDto>(_carDal.GetCarDetails(id));
        }

        public IDataResult<List<Car>> GetAllByFilter(int brandId, int colorId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll
                (c=> c.BrandId == brandId && c.ColorId == colorId));
        }

        public IDataResult<List<GetAllCarDto>> GetAllWithoutRents()
            // String rentDay, String returnDay -->. 
        {
            var rentalList = _circular.Value.GetAll();
            List<int> carIds = new List<int>();

            var cars = _carDal.GetAllCarDtos();
            var newList = new List<GetAllCarDto>();

            foreach (var item in rentalList.Data)
            {
                if((DateTime.Now>=item.RentDate && DateTime.Now <= item.ReturnDate))
                {
                    carIds.Add(item.CarId);
                }
            }

            foreach (var car in cars)
            {
                if(!carIds.Contains(car.Id))
                {
                    newList.Add(car);
                }
            }

            return new SuccessDataResult<List<GetAllCarDto>>(newList);
        }

        public IDataResult<List<Car>> GetAll(Expression<Func<Car,bool>>? filter = null)
        {
            return filter == null ? new SuccessDataResult<List<Car>>(_carDal.GetAll()):
                new SuccessDataResult<List<Car>>(_carDal.GetAll(filter));
        }
    }
}