using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Helpers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImagesDAL _carImagesDAL;

        public CarImageManager(ICarImagesDAL carImagesDAL)
        {
            _carImagesDAL = carImagesDAL;
        }
        [ValidationAspect(typeof(CarImageValidator))]
        public IResult Add(IFormFile file, CarImages carImages)
        {
            IResult result = BusinessRules.Run(CheckImageLimitExceeded(carImages.CarId));
            if (result != null)
            {
                return result;
            }
            carImages.ImagePath = FileHelper.Add(file);
            carImages.Date = DateTime.Now;
            _carImagesDAL.Add(carImages);
            return new SuccessResult();
        }
        [ValidationAspect(typeof(CarImageValidator))]
        public IResult Delete(CarImages carImages)
        {
            IResult result = BusinessRules.Run(CarImageDelete(carImages));
            if (result != null)
            {
                return result;
            }
            _carImagesDAL.Delete(carImages);
            return new SuccessResult();
        }
        [ValidationAspect(typeof(CarImageValidator))]
        public IResult Update(IFormFile file, CarImages carImages)
        {
            carImages.ImagePath = FileHelper.Update(_carImagesDAL.Get(p => p.Id == carImages.Id).ImagePath, file);
            carImages.Date = DateTime.Now;
            _carImagesDAL.Update(carImages);
            return new SuccessResult();
        }
        [ValidationAspect(typeof(CarImageValidator))]
        public IDataResult<CarImages> Get(int id)
        {
            return new SuccessDataResult<CarImages>(_carImagesDAL.Get(p => p.Id == id));
        }
        public IDataResult<List<CarImages>> GetAll()
        {
            return new SuccessDataResult<List<CarImages>>(_carImagesDAL.GetAll());
        }
        [ValidationAspect(typeof(CarImageValidator))]
        public IDataResult<List<CarImages>> GetImagesByCarId(int id)
        {
            return new SuccessDataResult<List<CarImages>>(CheckIfCarImageNull(id));
        }

        //business rules
        private IResult CheckImageLimitExceeded(int carid)
        {
            var carImagecount = _carImagesDAL.GetAll(p => p.CarId == carid).Count;
            if (carImagecount >= 5)
            {
                return new ErrorResult(Messages.CarImageLimitExceeded);
            }

            return new SuccessResult();
        }
        private List<CarImages> CheckIfCarImageNull(int id)
        {
            string path = @"\Images\default.png";
            var result = _carImagesDAL.GetAll(c => c.CarId == id).Any();
            if (!result)
            {
                return new List<CarImages> { new CarImages { CarId = id, ImagePath = path, Date = DateTime.Now } };
            }
            return _carImagesDAL.GetAll(p => p.CarId == id);
        }
        private IResult CarImageDelete(CarImages carImages)
        {
            try
            {
                File.Delete(carImages.ImagePath);
            }
            catch (Exception exception)
            {

                return new ErrorResult(exception.Message);
            }

            return new SuccessResult();
        }
    }
}
