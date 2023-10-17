using Core.DataAccess;
using Entities.Conceretes;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstracts
{
    public interface ICarDal:IEntityRepository<Car>
    {
        List<GetAllCarDto> GetAll();
        List<CarDetailDto> GetCarDetails();
    }
}