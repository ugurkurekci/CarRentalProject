using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICarImageService
    {
        IResult Add(CarImages carImages, IFormFile file);
        IResult Delete(CarImages carImage);
        IResult Update(CarImages carImages, IFormFile file);
        IDataResult<CarImages> Get(int id);
        IDataResult<List<CarImages>> GetAll();

        IDataResult<List<CarImages>> GetImagesByCarId(int id);
        IDataResult<List<CarImages>> GetDetailsByCarId(int id);


    }
}
