using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IRentalsDAL : IEntityRepository<Rentals>
    {
        List<RentalDetailsDto> GetRentDetails(Expression<Func<Rentals, bool>> filter = null);
    }
}
