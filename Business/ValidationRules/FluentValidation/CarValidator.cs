using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class CarValidator : AbstractValidator<Car>
    {
        public CarValidator()
        {

           
            RuleFor(p => p.BrandId).NotEmpty();
            RuleFor(p => p.ColorId).NotEmpty();
            RuleFor(p => p.DailyPrice).NotEmpty().GreaterThan(1000);
            RuleFor(p => p.Description).MinimumLength(2).NotEmpty();
            RuleFor(p => p.ModelYear).GreaterThan(1950).NotEmpty();

        }
    }
}
