using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDAL _carDAL;

        public CarManager(ICarDAL carDAL)
        {
            _carDAL = carDAL;
        }

        public IResult Add(Car Car)
        {

            _carDAL.Add(Car);
            return new SuccessResult(Messages.Success);

        }

        public IResult Delete(Car Car)
        {
            _carDAL.Delete(Car);
            return new SuccessResult(Messages.Success);
        }

        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(_carDAL.GetAll());
        }

        public IDataResult<Car> GetCarById(int id)
        {
            return new SuccessDataResult<Car>(_carDAL.Get(x => x.Id == id));
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int brandId)
        {
            return new SuccessDataResult<List<Car>>(_carDAL.GetAll().Where(x => x.BrandId == brandId).ToList());
        }

        public IDataResult<List<Car>> GetCarsByColorId(int colorId)
        {
            return new SuccessDataResult<List<Car>>(_carDAL.GetAll().Where(x => x.ColorId == colorId).ToList());
        }

        public IDataResult<List<Car>> GetCarsByDailyPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Car>>(_carDAL.GetAll(X => X.DailyPrice >= min && X.DailyPrice <= max));
        }

        public IDataResult<List<Car>> GetCarsByModelYear(string modelYear)
        {
            return new SuccessDataResult<List<Car>>(_carDAL.GetAll(X => X.ModelYear.ToString().Contains(modelYear)));
        }

        public IDataResult<List<CarDetailsDto>> GetCarDetails()
        {
            return new SuccessDataResult<List<CarDetailsDto>>(_carDAL.GetCarDetails());
        }

        public IResult Update(Car Car)
        {

            _carDAL.Update(Car);
            return new SuccessResult(Messages.Success);

        }
    }
}
