using Business.Abstracts;
using Business.BusinessRules;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using Entities.Conceretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Conceretes
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;
        IRentalBusinessRules _rentalBusinessRules;

        public RentalManager(IRentalDal rentalDal, IRentalBusinessRules rentalBusinessRules)
        {
            _rentalDal = rentalDal;
            _rentalBusinessRules = rentalBusinessRules;
        }

        [ValidationAspect(typeof(RentalValidator))]
        public IDataResult<List<IResult>> Add(Rental rental)
        {

            var errorResults = Rules.Run(_rentalBusinessRules
                .checkIfRentalCarReturnDateIsNull(rental.CarId));

            if(errorResults.Any())
            {
                return new ErrorDataResult<List<IResult>>(errorResults);
            }

            _rentalDal.Add(rental);

            List<IResult> result = new List<IResult>();
            result.Add(new SuccessDataResult<Rental>(rental));

            return new SuccessDataResult<List<IResult>>(result);
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

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll());
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
    }
}