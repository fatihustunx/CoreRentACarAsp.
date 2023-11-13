using Business.Abstracts;
using Business.BusinessRules;
using Business.BusinessRules.Abstracts;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Constants;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using Entities.Conceretes;
using Entities.DTOs;
using Entities.DTOs.Requests;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Conceretes
{
    public class RentalManager : IRentalService
    {
        ICarService _carService;
        IRentalDal _rentalDal;
        IRentalBusinessRules _rentalBusinessRules;

        public RentalManager(IRentalDal rentalDal, IRentalBusinessRules rentalBusinessRules,
            ICarService carService)
        {
            _carService = carService;
            _rentalDal = rentalDal;
            _rentalBusinessRules = rentalBusinessRules;
        }

        [ValidationAspect(typeof(RentalForAddValidator))]
        public IDataResult<GetRentForAddDto> Add(RentalForAdd rentalForAdd)
        {
            var rentDay = Operations.Parser(rentalForAdd.RentDate);
            var returnDay = Operations.Parser(rentalForAdd.ReturnDate);

            var errorResults = Rules.Run(
                _rentalBusinessRules.checkIfRentalCarRentDateIsPass(rentalForAdd.CarId,rentDay),
                _rentalBusinessRules.checkIfRentalCarReturnDateIsPass(rentalForAdd.CarId,returnDay)
                );

            if(errorResults != null)
            {
                return errorResults;
            }

            var diff = returnDay - rentDay;

            var price = _carService.Get(rentalForAdd.CarId).Data.DailyPrice;

            var rentalForAdded = new Rental
            {
                CarId = rentalForAdd.CarId,
                CustomerId = rentalForAdd.CustomerId,
                RentDate = rentDay,
                ReturnDate = returnDay,
                TotalCost = diff.Days*price,
                IsState = false
            };

            _rentalDal.Add(rentalForAdded);

            var getRentForAdd = new GetRentForAddDto
            {
                Id = rentalForAdded.Id,
                TotalCost = rentalForAdded.TotalCost
            };

            return new SuccessDataResult<GetRentForAddDto>(getRentForAdd);
        }

        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult();
        }

        public IDataResult<Rental> Get(int id)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(r => r.Id == id));
        }

        public IDataResult<List<GetAllRentalDto>> GetAllRentalDtos()
        {
            return new SuccessDataResult<List<GetAllRentalDto>>(_rentalDal.GetAllRentalDtos());
        }

        public IDataResult<List<Rental>> GetAllByCarId(int carId)
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(r => r.CarId == carId));
        }

        public IDataResult<List<Rental>> GetAllByCustomerId(int customerId)
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(r => r.CustomerId == customerId));
        }

        public IResult Update(Rental rental)
        {
            _rentalDal.Update(rental);
            return new SuccessResult();
        }

        public IDataResult<List<Rental>> GetAll(Expression<Func<Rental,bool>>? filter = null)
        {
            return filter == null ? new SuccessDataResult<List<Rental>>(_rentalDal.GetAll()):
                new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(filter));
        }
    }
}