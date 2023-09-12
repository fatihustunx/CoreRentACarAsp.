﻿using Core.DataAccess.EntityFramework;
using DataAccess.Abstracts;
using DataAccess.Contexts;
using Entities.Conceretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Conceretes.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User,RentACarContext>,IUserDal
    {

    }
}