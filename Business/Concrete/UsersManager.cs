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
    public class UsersManager : IUsersService
    {
        IUsersDAL _usersDAL;

        public UsersManager(IUsersDAL usersDAL)
        {
            _usersDAL = usersDAL;
        }

        public IResult Add(Users users)
        {
            _usersDAL.Add(users);
            return new SuccessResult(Messages.Success);
        }

        public IResult Delete(Users users)
        {
            _usersDAL.Delete(users);
            return new SuccessResult(Messages.Success);
        }

        public IDataResult<List<Users>> GetAll()
        {

            return new SuccessDataResult<List<Users>>(_usersDAL.GetAll());
        }

        public IDataResult<Users> GetById(int id)
        {
            return new SuccessDataResult<Users>(_usersDAL.Get(X => X.UserId == id));
        }

        public IResult Update(Users users)
        {
            _usersDAL.Update(users);
            return new SuccessResult(Messages.Success);
        }
    }
}
