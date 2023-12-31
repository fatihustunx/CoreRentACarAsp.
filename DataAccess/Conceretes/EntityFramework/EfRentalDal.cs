﻿using Core.DataAccess.EntityFramework;
using DataAccess.Abstracts;
using DataAccess.Conceretes.EntityFramework.Contexts;
using Entities.Conceretes;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Conceretes.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, RentACarContext>, IRentalDal
    {
        public List<GetAllRentalDto> GetAllRentalDtos()
        {
            using (RentACarContext context = new RentACarContext())
            {
                var result = from r in context.Rentals
                             join c in context.Cars
                             on r.CarId equals c.Id
                             join b in context.Brands
                             on c.BrandId equals b.Id
                             join cr in context.Customers
                             on r.CustomerId equals cr.Id
                             join u in context.Users
                             on cr.UserId equals u.Id

                             where r.IsState.Equals(true)

                             select new GetAllRentalDto { Id = r.Id, BrandName = b.Name,
                                 FullName = $"{u.FirstName}{" "}{u.LastName}",
                                 RentDate = r.RentDate, ReturnDate = r.ReturnDate, TotalCost = r.TotalCost };


                return result.ToList();
            }
        }
    }
}