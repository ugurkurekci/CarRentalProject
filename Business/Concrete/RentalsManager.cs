using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
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



        public IResult Update(Rentals rentals)
        {
            _rentalsDAL.Update(rentals);
            return new SuccessResult(Messages.Success);
        }
    }
}
