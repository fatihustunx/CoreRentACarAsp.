using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.BusinessRules.Abstracts
{
    public interface ICarImageBusinessRules
    {
        IResult CheckIfCarImagesLimitIsCorrect(int carId);

        IResult CheckIfFormFileIsExist(long length);
        //IResult CheckIfDirectoryIsExist(string root);
    }
}