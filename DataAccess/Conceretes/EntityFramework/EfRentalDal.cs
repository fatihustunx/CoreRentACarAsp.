using Core.DataAccess.EntityFramework;
using DataAccess.Abstracts;
using DataAccess.Conceretes.EntityFramework.Contexts;
using Entities.Conceretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Conceretes.EntityFramework
{
    public class EfRentalDal:EfEntityRepositoryBase<Rental,RentACarContext>, IRentalDal
    {

    }
}