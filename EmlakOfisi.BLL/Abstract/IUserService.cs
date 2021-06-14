using EmlakOfisi.Core.Utilities.Results;
using EmlakOfisi.Entities.Concrete;
using EmlakOfisi.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EmlakOfisi.BLL.Abstract
{
    public interface IUserService
    {
        Task<IDataResult<User>> Login(UserSignInViewModel userSignInViewModel);
        Task<IResult> SignOut();
        Task<IDataResult<User>> Register(UserSignUpViewModel userSignUpViewModel);
        Task<IDataResult<User>> ChangePassword(UserChangePasswordViewModel userChangePasswordViewModel,int userId);
    }
}
