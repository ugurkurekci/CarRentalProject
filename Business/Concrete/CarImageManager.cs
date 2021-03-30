using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Helpers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
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
        //   [SecuredOperation("Admin")]
        //   [CacheRemoveAspect("ICarImageService.Get")]
        // [ValidationAspect(typeof(CarImageValidator))]
        public IResult Add(CarImages carImages, IFormFile file)
        {

            var imageCount = _carImagesDAL.GetAll(c => c.CarId == carImages.CarId).Count;

            if (imageCount >= 5)
            {
                return new ErrorResult("One car must have 5 or less images");
            }

            var imageResult = FileHelper.FileUploadHelper.CreateImage(file);

            if (!imageResult.Success)
            {
                return new ErrorResult(imageResult.Message);
            }
            carImages.ImagePath = imageResult.Message;
            _carImagesDAL.Add(carImages);
            return new SuccessResult("Car image added");
        }

        // [SecuredOperation("Admin")]
        [ValidationAspect(typeof(CarImageValidator))]
        public IResult Delete(CarImages carImages)
        {
            var image = _carImagesDAL.Get(c => c.Id == carImages.Id);
            if (image == null)
            {
                return new ErrorResult("Image not found");
            }

            FileHelper.FileUploadHelper.DeleteImage(image.ImagePath);
            _carImagesDAL.Delete(carImages);
            return new SuccessResult("Image was deleted successfully");
        }
        // [SecuredOperation("Admin")]
        [CacheRemoveAspect("ICarImageService.Get")]
        [ValidationAspect(typeof(CarImageValidator))]
        public IResult Update(CarImages carImages, IFormFile file)
        {
            var isImage = _carImagesDAL.Get(c => c.Id == carImages.Id);
            if (isImage == null)
            {
                return new ErrorResult("Image not found");
            }

            var updatedFile = FileHelper.FileUploadHelper.UpdateImage(isImage.ImagePath,file);
            if (!updatedFile.Success)
            {
                return new ErrorResult(updatedFile.Message);
            }
            carImages.ImagePath = updatedFile.Message;
            _carImagesDAL.Update(carImages);
            return new SuccessResult("Car image updated");
        }
        //  [SecuredOperation("Admin")]
        [CacheAspect(duration: 10)]
        [ValidationAspect(typeof(CarImageValidator))]
        public IDataResult<CarImages> Get(int id)
        {
            return new SuccessDataResult<CarImages>(_carImagesDAL.Get(p => p.Id == id));
        }
        //  [SecuredOperation("Admin")]
        [CacheAspect(duration: 60)]
        public IDataResult<List<CarImages>> GetAll()
        {
            return new SuccessDataResult<List<CarImages>>(_carImagesDAL.GetAll());
        }
        //  [SecuredOperation("Admin")]
        [ValidationAspect(typeof(CarImageValidator))]
        [CacheAspect(duration: 10)]
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
            string path = @"C:\Users\ugurk\Documents\GitHub\CarRentalProject\WebAPI\wwwroot\default.png";
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
