using Business.BusinessRules.Abstracts;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.BusinessRules.Conceretes
{
    public class CarImageBusinessRules : ICarImageBusinessRules
    {
        ICarImageDal _carImageDal;

        public CarImageBusinessRules(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }

        public IResult CheckIfCarImagesLimitIsCorrect(int carId)
        {
            var result = _carImageDal.GetAll(i => i.CarId == carId);

            if (result.Count >= 5)
            {
                return new ErrorResult(Messages.CarImagesLimitExceded);
            }
            return new SuccessResult();
        }

        public IResult CheckIfFormFileIsExist(long length)
        {
            if(length > 0)
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }
    }
}