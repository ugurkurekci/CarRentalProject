using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IRentalsServices
    {
        IResult Add(Rentals rentals);
        IResult Delete(Rentals rentals);
        IResult Update(Rentals rentals);
        IDataResult<List<Rentals>> GetAll();
        IDataResult<Rentals> GetById(int id);
        
    }
}
