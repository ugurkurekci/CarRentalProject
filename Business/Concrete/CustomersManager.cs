using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CustomersManager : ICustomersService
    {
        ICustomersDAL _customersDAL;

        public CustomersManager(ICustomersDAL customersDAL)
        {
            _customersDAL = customersDAL;
        }
        [CacheRemoveAspect("ICustomersService.Get")]
        [SecuredOperation("Admin")]
        [ValidationAspect(typeof(CustomersValidator))]
        public IResult Add(Customers customers)
        {
            _customersDAL.Add(customers);
            return new SuccessResult(Messages.Success);
        }
        [SecuredOperation("Admin")]
        public IResult Delete(Customers customers)
        {
            _customersDAL.Delete(customers);
            return new SuccessResult(Messages.Success);
        }
        [SecuredOperation("Admin")]
        [CacheAspect(duration: 60)]
        public IDataResult<List<Customers>> GetAll()
        {

            return new SuccessDataResult<List<Customers>>(_customersDAL.GetAll(), Messages.Success);
        }
        [SecuredOperation("Admin")]
        [CacheAspect(duration: 10)]
        public IDataResult<Customers> GetById(int id)
        {
            return new SuccessDataResult<Customers>(_customersDAL.Get(c => c.CustomerId == id));

        }
        [SecuredOperation("Admin")]
        [CacheRemoveAspect("ICustomersService.Get")]
        public IResult Update(Customers customers)
        {
            _customersDAL.Update(customers);
            return new SuccessResult(Messages.Success);
        }
    }
}
