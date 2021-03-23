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
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        IBrandDAL _brandDAL;

        public BrandManager(IBrandDAL branDAL)
        {
            _brandDAL = branDAL;
        }
        //[SecuredOperation("Admin")]
        [CacheRemoveAspect("IBrandService.Get")]
        [ValidationAspect(typeof(BrandValidator))]
        public IResult Add(Brand brand)
        {

            _brandDAL.Add(brand);
            return new SuccessResult(Messages.Success);
        }


        //[SecuredOperation("Admin")]
        public IResult Delete(Brand brand)
        {

            _brandDAL.Delete(brand);
            return new SuccessResult(Messages.Success);
        }


        //[SecuredOperation("Admin")]
        [CacheAspect(duration: 60)]
        public IDataResult<List<Brand>> GetAll()
        {

            return new SuccessDataResult<List<Brand>>(_brandDAL.GetAll());
        }


        //[SecuredOperation("Admin")]
        [CacheAspect(duration: 10)]
        public IDataResult<Brand> GetBrandById(int brandId)
        {
            return new SuccessDataResult<Brand>(_brandDAL.Get(x => x.BrandId == brandId));
        }


        //[SecuredOperation("Admin")]
        [CacheRemoveAspect("IBrandService.Get")]
        [ValidationAspect(typeof(BrandValidator))]
        public IResult Update(Brand brand)
        {

            _brandDAL.Update(brand);
            return new SuccessResult(Messages.Success);





        }
    }
}
