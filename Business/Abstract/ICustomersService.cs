using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICustomersService
    {
        IResult Add(Customers customers);
        IResult Delete(Customers customers);
        IResult Update(Customers customers);
        IDataResult<List<Customers>> GetAll();
        IDataResult<Customers> GetById(int id);
    }
}
