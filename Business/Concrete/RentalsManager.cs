using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
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
        [ValidationAspect(typeof(RentalsValidator))]
        public IResult Add(Rentals rentals)
        {
            _rentalsDAL.Add(rentals);
            return new SuccessResult(Messages.Success);
        }

        public IResult Delete(Rentals rentals)
        {
            _rentalsDAL.Delete(rentals);
            return new SuccessResult(Messages.Success);
        }

        public IDataResult<List<Rentals>> GetAll()
        {
            return new SuccessDataResult<List<Rentals>>(_rentalsDAL.GetAll());
        }

        public IDataResult<Rentals> GetById(int id)
        {
            return new SuccessDataResult<Rentals>(_rentalsDAL.Get(X => X.RentalId == id));
        }

        public IDataResult<List<RentalDetailsDto>> GetRentDetails(Expression<Func<Rentals, bool>> filter = null)
        {
            return new SuccessDataResult<List<RentalDetailsDto>>(_rentalsDAL.GetRentDetails(), Messages.Success);
        }
        [ValidationAspect(typeof(RentalsValidator))]

        public IResult Update(Rentals rentals)
        {
            _rentalsDAL.Update(rentals);
            return new SuccessResult(Messages.Success);
        }
    }
}
