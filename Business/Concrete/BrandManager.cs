using Business.Abstract;
using Business.Constants;
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

        public IResult Add(Brand brand)
        {

            _brandDAL.Add(brand);
            return new SuccessResult(Messages.Success);
        }

        public IResult Delete(Brand brand)
        {

            _brandDAL.Delete(brand);
            return new SuccessResult(Messages.Success);
        }

        public IDataResult<List<Brand>> GetAll()
        {

            return new SuccessDataResult<List<Brand>>(_brandDAL.GetAll());
        }

        public IDataResult<Brand> GetBrandById(int brandId)
        {
            return new SuccessDataResult<Brand>(_brandDAL.Get(x => x.BrandId == brandId));
        }

        public IResult Update(Brand brand)
        {

            _brandDAL.Update(brand);
            return new SuccessResult(Messages.Success);





        }
    }
}
