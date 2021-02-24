using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Business.Concrete
{
    public class ColorManager : IColorService
    {
        IColorDAL _colorDAL;

        public ColorManager(IColorDAL colorDAL)
        {
            _colorDAL = colorDAL;
        }

        [ValidationAspect(typeof(ColorValidator))]
        public IResult Add(Entities.Concrete.Color color)
        {
            _colorDAL.Add(color);
            return new SuccessResult(Messages.Success);
        }

        public IResult Delete(Entities.Concrete.Color color)
        {
            _colorDAL.Delete(color);
            return new SuccessResult(Messages.Success);
        }

        public IDataResult<List<Entities.Concrete.Color>> GetAll()
        {
            return new SuccessDataResult<List<Entities.Concrete.Color>>(_colorDAL.GetAll());
        }

        public IDataResult<Entities.Concrete.Color> GetColorById(int colorId)
        {
            return new SuccessDataResult<Entities.Concrete.Color>(_colorDAL.Get(x => x.ColorId == colorId));
        }
        [ValidationAspect(typeof(ColorValidator))]
        public IResult Update(Entities.Concrete.Color color)
        {
            _colorDAL.Update(color);
            return new SuccessResult(Messages.Success);
        }
    }
}
