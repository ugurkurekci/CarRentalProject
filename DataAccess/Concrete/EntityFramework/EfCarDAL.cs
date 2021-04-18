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
        public List<CarDetailsDto> GetCarDetails(Expression<Func<Car, bool>> filter = null)
        {
            using (CarDbContext contex = new CarDbContext())
            {
                var result = from c in filter == null ? contex.Car : contex.Car.Where(filter)
                             join b in contex.Brand
                             on c.BrandId equals b.BrandId
                             join cl in contex.Color
                             on c.ColorId equals cl.ColorId
                             join ci in contex.CarImages
                             on c.Id equals ci.CarId
                             select new CarDetailsDto
                             {
                                 Id = c.Id,
                                 BrandId = c.BrandId,
                                 ColorId = c.ColorId,
                                 DailyPrice = c.DailyPrice,
                                 ModelYear = c.ModelYear,
                                 Description = c.Description,                                
                                 BrandName = b.BrandName,
                                 ColorName = cl.ColorName,
                                 ImagePath=ci.ImagePath,
                                 ImageId=ci.Id,
                                 Date=ci.Date,
                                 CarName=ci.CarId
                                 
                                 
                                 
                             };
                return result.ToList();
            }
        }

        
        
    }
}