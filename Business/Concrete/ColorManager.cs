using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
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
      //  [SecuredOperation("Admin")]
        [CacheRemoveAspect("IColorService.Get")]
        [ValidationAspect(typeof(ColorValidator))]
        public IResult Add(Entities.Concrete.Color color)
        {
            _colorDAL.Add(color);
            return new SuccessResult(Messages.Success);
        }
        [SecuredOperation("Admin")]
        public IResult Delete(Entities.Concrete.Color color)
        {
            _colorDAL.Delete(color);
            return new SuccessResult(Messages.Success);
        }
        //[SecuredOperation("Admin")]
        [CacheAspect(duration: 60)]
        public IDataResult<List<Entities.Concrete.Color>> GetAll()
        {
            return new SuccessDataResult<List<Entities.Concrete.Color>>(_colorDAL.GetAll());
        }
        [SecuredOperation("Admin")]
        [CacheAspect(duration: 10)]
        public IDataResult<Entities.Concrete.Color> GetColorById(int colorId)
        {
            return new SuccessDataResult<Entities.Concrete.Color>(_colorDAL.Get(x => x.ColorId == colorId));
        }

        [SecuredOperation("Admin")]
        [ValidationAspect(typeof(ColorValidator))]
        [CacheRemoveAspect("IColorService.Get")]
        public IResult Update(Entities.Concrete.Color color)
        {
            _colorDAL.Update(color);
            return new SuccessResult(Messages.Success);
        }
    }
}
