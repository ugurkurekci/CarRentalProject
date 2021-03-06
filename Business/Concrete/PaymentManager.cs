﻿using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class PaymentManager : IPaymentService
    {


        private Payment _payment = new Payment
        {
            FirstName = "A",
            LastName = "A",
            CardNumber = 12345678,
            CVV = 123,
            MonthOfExpiration = 1,
            YearOfExpiration = 1
        };
        public IResult Check(Payment payment)
        {
            if (_payment.FirstName == payment.FirstName && _payment.LastName == payment.LastName &&
               _payment.CardNumber == payment.CardNumber && _payment.CVV == payment.CVV &&
           _payment.MonthOfExpiration == payment.MonthOfExpiration && _payment.YearOfExpiration == payment.YearOfExpiration)

                return new SuccessResult(Messages.PaymentSuccess);
            else { 
                return new ErrorResult(Messages.PaymentError);
            }
        }
    }
}
