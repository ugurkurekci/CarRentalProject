using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalsDAL : EfEntityRepositoryBase<Rentals, CarDbContext>, IRentalsDAL
    {
        public List<RentalDetailsDto> GetRentDetails(Expression<Func<Rentals, bool>> filter = null)
        {
            using (CarDbContext contex = new CarDbContext())
            {
				var result = from r in filter is null ? contex.Rentals : contex.Rentals.Where(filter)
							 join c in contex.Car on r.CarId equals c.Id
							 join b in contex.Brand on c.BrandId equals b.BrandId
							 join cu in contex.Customers on r.CustomerId equals cu.CustomerId
							 join u in contex.Users on cu.UserId equals u.Id
							 select new RentalDetailsDto
							 {
								 Id = r.CarId,
								 BrandName = b.BrandName,								
								 FirstName = u.FirstName,
								 LastName = u.LastName,
								 DailyPrice = c.DailyPrice,
								 RentDate = r.RentDate,
								 ReturnDate = r.ReturnDate
							 };
				return result.ToList();

            }
        }
    }
}


