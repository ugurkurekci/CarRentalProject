using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICarService
    {
        IResult Add(Car Car);
        IResult Update(Car Car);
        IResult Delete(Car Car);
        IDataResult<List<Car>> GetAll();
        IDataResult<Car> GetCarById(int id);
        IDataResult<List<Car>> GetCarsByDailyPrice(decimal min, decimal max);
        IDataResult<List<Car>> GetCarsByModelYear(string modelYear);
        IDataResult<List<Car>> GetCarsByBrandId(int brandId);
        IDataResult<List<Car>> GetCarsByColorId(int colorId);


        IDataResult<List<CarDetailsDto>> GetCarDetails();

        IDataResult<List<CarDetailsDto>> GetCarDetailsById(int carId);
        IDataResult<List<CarDetailsDto>> GetCarDetailsByBrandAndColorId(int brandId, int colorId);

        IDataResult<List<CarDetailsDto>> GetCarDetailsFilter(int brandId, int colorId);
        IDataResult<List<CarDetailsDto>> GetCarsByBrandIdList(int brandId);
        IDataResult<List<CarDetailsDto>> GetCarsByColorIdList(int colorId);
        IDataResult<List<CarDetailsDto>> GetCarDetailsByCarId(int carId);


    }
}
