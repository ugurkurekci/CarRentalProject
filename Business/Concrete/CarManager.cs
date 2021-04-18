using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDAL _carDAL;

        public CarManager(ICarDAL carDAL)
        {
            _carDAL = carDAL;
        }
        // [SecuredOperation("Admin")]
        [CacheRemoveAspect("ICarService.Get")]
        [ValidationAspect(typeof(CarValidator))]
        public IResult Add(Car Car)
        {

            _carDAL.Add(Car);
            return new SuccessResult(Messages.Success);

        }
        //  [SecuredOperation("Admin")]
        public IResult Delete(Car Car)
        {
            _carDAL.Delete(Car);
            return new SuccessResult(Messages.Success);
        }
        // [SecuredOperation("Admin")]
        [CacheAspect(duration: 60)]
        public IDataResult<List<Car>> GetAll()
        {
            Thread.Sleep(1500);
            return new SuccessDataResult<List<Car>>(_carDAL.GetAll());
        }
        //[SecuredOperation("Admin")]
        [PerformanceAspect(5)]
        public IDataResult<List<CarDetailsDto>> GetCarDetails()
        {
            Thread.Sleep(1500);
            return new SuccessDataResult<List<CarDetailsDto>>(_carDAL.GetCarDetails(), Messages.Success);
        }
        //   [SecuredOperation("Admin")]
        [CacheAspect(duration: 10)]
        public IDataResult<Car> GetCarById(int id)
        {
            return new SuccessDataResult<Car>(_carDAL.Get(x => x.Id == id));
        }
        //  [SecuredOperation("Admin")]
        [CacheAspect(duration: 10)]
        public IDataResult<List<Car>> GetCarsByBrandId(int brandId)
        {
            return new SuccessDataResult<List<Car>>(_carDAL.GetAll(x => x.BrandId == brandId));
        }
        //  [SecuredOperation("Admin")]
        [CacheAspect(duration: 10)]
        public IDataResult<List<Car>> GetCarsByColorId(int colorId)
        {
            return new SuccessDataResult<List<Car>>(_carDAL.GetAll(x => x.ColorId == colorId));
        }
        // [SecuredOperation("Admin")]
        [PerformanceAspect(5)]
        public IDataResult<List<Car>> GetCarsByDailyPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Car>>(_carDAL.GetAll(X => X.DailyPrice >= min && X.DailyPrice <= max));
        }
        // [SecuredOperation("Admin")]
        [CacheAspect(duration: 10)]
        public IDataResult<List<Car>> GetCarsByModelYear(string modelYear)
        {
            return new SuccessDataResult<List<Car>>(_carDAL.GetAll(X => X.ModelYear.ToString().Contains(modelYear)));
        }

        //  [SecuredOperation("Admin")]
        [CacheRemoveAspect("ICarService.Get")]
        [ValidationAspect(typeof(CarValidator))]
        public IResult Update(Car Car)
        {

            _carDAL.Update(Car);
            return new SuccessResult(Messages.Success);

        }

        public IDataResult<List<CarDetailsDto>> GetCarDetailsById(int carId)
        {
            return new SuccessDataResult<List<CarDetailsDto>>(_carDAL.GetCarDetails(c => c.Id == carId));
        }

        public IDataResult<List<CarDetailsDto>> GetCarDetailsByBrandAndColorId(int brandId, int colorId)
        {
            return new SuccessDataResult<List<CarDetailsDto>>(_carDAL.GetCarDetails(c => c.BrandId == brandId && c.ColorId == colorId));
        }

        public IDataResult<List<CarDetailsDto>> GetCarDetailsFilter(int brandId, int colorId)
        {
            return new SuccessDataResult<List<CarDetailsDto>>(
                _carDAL.GetCarDetails(c => c.BrandId == brandId && c.ColorId == colorId));
        }

        public IDataResult<List<CarDetailsDto>> GetCarsByBrandIdList(int brandId)
        {
            return new SuccessDataResult<List<CarDetailsDto>>(_carDAL.GetCarDetails(c => c.BrandId == brandId));
        }

        public IDataResult<List<CarDetailsDto>> GetCarsByColorIdList(int colorId)
        {
            return new SuccessDataResult<List<CarDetailsDto>>(_carDAL.GetCarDetails(c => c.ColorId == colorId));
        }

        public IDataResult<List<CarDetailsDto>> GetCarDetailsByCarId(int carId)
        {
            return new SuccessDataResult<List<CarDetailsDto>>(_carDAL.GetCarDetails(c => c.Id == carId));
        }
    }
}
