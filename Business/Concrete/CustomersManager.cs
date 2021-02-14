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
    public class CustomersManager : ICustomersService
    {
        ICustomersDAL _customersDAL;

        public CustomersManager(ICustomersDAL customersDAL)
        {
            _customersDAL = customersDAL;
        }

        public IResult Add(Customers customers)
        {
            _customersDAL.Add(customers);
            return new SuccessResult(Messages.Success);
        }

        public IResult Delete(Customers customers)
        {
            _customersDAL.Delete(customers);
            return new SuccessResult(Messages.Success);
        }

        public IDataResult<List<Customers>> GetAll()
        {

            return new SuccessDataResult<List<Customers>>(_customersDAL.GetAll(), Messages.Success);
        }

        public IDataResult<Customers> GetById(int id)
        {
            return new SuccessDataResult<Customers>(_customersDAL.Get(c => c.CustomerId == id));

        }

        public IResult Update(Customers customers)
        {
            _customersDAL.Update(customers);
            return new SuccessResult(Messages.Success);
        }
    }
}
