using Business.Abstracts;
using Business.BusinessRules.Abstracts;
using Business.Constants;
using Core.Utilities.AFiles;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using Entities.Conceretes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Conceretes
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;
        ICarImageBusinessRules _carImageBusinessRules;

        IFileOperations _fileOperations;

        public CarImageManager(ICarImageDal carImageDal,
            ICarImageBusinessRules carImageBusinessRules,
            IFileOperations fileOperations)
        {
            _carImageDal = carImageDal;
            _carImageBusinessRules = carImageBusinessRules;
            _fileOperations = fileOperations;
        }

        public IResult Add(IFormFile formFile,CarImage carImage)
        {
            var result = Rules.Run(_carImageBusinessRules.CheckIfCarImagesLimitIsCorrect(carImage.CarId),
                _carImageBusinessRules.CheckIfFormFileIsExist(formFile.Length));

            if(result != null)
            {
                return result;
            }

            carImage.ImagePath = _fileOperations
                .Upload(formFile,FilePaths.FilePath);
            carImage.Date = DateTime.Now;

            _carImageDal.Add(carImage);
            return new SuccessResult();
        }

        public IResult Delete(CarImage carImage)
        {
            CarImage img = Get(carImage.Id).Data;

            if(img != null)
            {
                _fileOperations.Delete(img.ImagePath,
               FilePaths.FilePath);

                _carImageDal.Delete(img);
            }

            return new SuccessResult();
        }

        public IDataResult<CarImage> Get(int id)
        { 
            return new SuccessDataResult<CarImage>(_carImageDal.Get(i => i.Id == id));
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
        }

        public IResult Update(IFormFile formFile, CarImage carImage)
        {
            CarImage img = Get(carImage.Id).Data;

            var result = Rules.Run(_carImageBusinessRules
                .CheckIfFormFileIsExist(formFile.Length));

            if (result != null)
            {
                return result;
            }

            img.ImagePath = _fileOperations
                .Update(formFile,img.ImagePath,
                FilePaths.FilePath);

            img.Date = DateTime.Now;

            _carImageDal.Update(img);
            return new SuccessResult();
        }

        public IDataResult<List<CarImage>> GetAllByCarId(int carId)
        {
            var result = _carImageDal.GetAll(i => i.CarId == carId);
            
            if(!result.Any())
            {
                List<CarImage> logoS = new List<CarImage>()
                { new CarImage { CarId=carId,
                    ImagePath=FilePaths.DefaultFiles, Date=DateTime.Now}};
                return new SuccessDataResult<List<CarImage>>(logoS);
            }

            return new SuccessDataResult<List<CarImage>>(result);
        }

    }
}