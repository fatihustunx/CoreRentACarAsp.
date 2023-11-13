using Core.DataAccess.EntityFramework;
using DataAccess.Abstracts;
using DataAccess.Conceretes.EntityFramework.Contexts;
using Entities.Conceretes;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Conceretes.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, RentACarContext>, ICarDal
    {
        public List<GetAllCarDto> GetAllCarDtos()
        {
            using (RentACarContext context = new RentACarContext())
            {
                var result = from c in context.Cars
                             join b in context.Brands
                             on c.BrandId equals b.Id
                             join clr in context.Colors
                             on c.ColorId equals clr.Id
                             select new GetAllCarDto
                             {
                                 Id = c.Id,
                                 BrandName = b.Name,
                                 ColorName = clr.Name,
                                 Name = c.Name,
                                 ModelYear = c.ModelYear,
                                 DailyPrice = c.DailyPrice,
                                 Description = c.Description
                             };

                return result.ToList();
            }
        }

        public CarDetailDto GetCarDetails(int id)
        {
            using (RentACarContext context = new RentACarContext())
            {
                var result = from c in context.Cars
                             where c.Id == id
                             join b in context.Brands
                             on c.BrandId equals b.Id
                             join clr in context.Colors
                             on c.ColorId equals clr.Id
                             select new CarDetailDto { Id = c.Id, BrandName = b.Name, ColorName = clr.Name, Name = c.Name,
                                 ModelYear = c.ModelYear, DailyPrice = c.DailyPrice, Description = c.Description };

#pragma warning disable CS8603 // Possible null reference return.
                return result.SingleOrDefault();
#pragma warning restore CS8603 // Possible null reference return.
            }
        }
    }
}