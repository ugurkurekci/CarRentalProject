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
                var result = from ca in contex.Car
                             join re in contex.Rentals
                             on ca.Id equals re.CarId
                             join cu in contex.Customers
                             on re.CustomerId equals cu.CustomerId
                             join us in contex.Users
                             on cu.UserId equals us.UserId
                             select new RentalDetailsDto
                             {
                                 RentalId = re.RentalId,
                                 CarId = ca.Id,
                                 CustomerName = cu.CustomerName,
                                 UserName = us.FirstName,
                                 RentDate = re.RentDate,
                                 ReturnDate = re.ReturnDate
                             };
                return result.ToList();

            }
        }
    }
}


