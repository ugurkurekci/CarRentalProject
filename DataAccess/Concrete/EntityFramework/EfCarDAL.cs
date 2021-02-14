using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDAL : EfEntityRepositoryBase<Car, CarDbContext>, ICarDAL
    {
        public List<CarDetailsDto> GetCarDetails()
        {
            using (CarDbContext contex = new CarDbContext())
            {
                var result = from c in contex.Car
                             join b in contex.Brand
                             on c.Id equals b.BrandId
                             join cl in contex.Color
                             on c.ColorId equals cl.ColorId
                             select new CarDetailsDto
                             {
                                 CarName = c.Description,
                                 BrandName = b.BrandName,
                                 ColorName = cl.ColorName,
                                 DailyPrice = c.DailyPrice
                             };
                return result.ToList();

            }
        }
    }
}