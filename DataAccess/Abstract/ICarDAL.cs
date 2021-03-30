﻿using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Abstract
{
    public interface ICarDAL : IEntityRepository<Car>
    {
        
        List<CarDetailsDto> GetCarDetails(Expression<Func<Car, bool>> filter = null);
        
    }
}
