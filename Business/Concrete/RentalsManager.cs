using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Business.Concrete
{
    public class RentalsManager : IRentalsServices
    {
        IRentalsDAL _rentalsDAL;

        public RentalsManager(IRentalsDAL rentalsDAL)
        {
            _rentalsDAL = rentalsDAL;
        }
        [SecuredOperation("Admin")]
        [CacheRemoveAspect("IRentalsService.Get")]
        [ValidationAspect(typeof(RentalsValidator))]
        public IResult Add(Rentals rentals)
        {
            var result = _rentalsDAL.GetAll(x => x.CarId == rentals.CarId);
            foreach (var item in result)
            {
                if (item.ReturnDate == null || item.RentDate > item.ReturnDate)
                {
                    return new ErrorResult(Messages.DataNone);
                }
            }

            _rentalsDAL.Add(rentals);
            return new SuccessResult(Messages.Success);
        }
        [TransactionScopeAspect]
        public IResult AddTransactionalTest(Rentals rentals)
        {
            _rentalsDAL.Update(rentals);
            _rentalsDAL.Add(rentals);
            return new SuccessResult();
        }

        [SecuredOperation("Admin")]
        public IResult Delete(Rentals rentals)
        {
            _rentalsDAL.Delete(rentals);
            return new SuccessResult(Messages.Success);
        }

        [SecuredOperation("Admin")]
        [CacheAspect(duration: 60)]
        public IDataResult<List<Rentals>> GetAll()
        {
            return new SuccessDataResult<List<Rentals>>(_rentalsDAL.GetAll());
        }
        [SecuredOperation("Admin")]
        [CacheAspect(duration: 10)]
        public IDataResult<Rentals> GetById(int id)
        {
            return new SuccessDataResult<Rentals>(_rentalsDAL.Get(X => X.RentalId == id));
        }

        [PerformanceAspect(5)]
        public IDataResult<List<RentalDetailsDto>> GetRentDetails(Expression<Func<Rentals, bool>> filter = null)
        {
            return new SuccessDataResult<List<RentalDetailsDto>>(_rentalsDAL.GetRentDetails(), Messages.Success);
        }

        [SecuredOperation("Admin")]
        [CacheRemoveAspect("IRentalsService.Get")]
        [ValidationAspect(typeof(RentalsValidator))]

        public IResult Update(Rentals rentals)
        {
            _rentalsDAL.Update(rentals);
            return new SuccessResult(Messages.Success);
        }
    }
}
