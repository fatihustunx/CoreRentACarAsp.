﻿using Core.DataAccess;
using Entities.Conceretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstracts
{
    public interface IUserDal : IEntityRepository<User>
    {
    }
}