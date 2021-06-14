using EmlakOfisi.BLL.Abstract;
using EmlakOfisi.Core.Utilities.Results;
using EmlakOfisi.DAL.Abstract;
using EmlakOfisi.Entities.Concrete;
using EmlakOfisi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EmlakOfisi.BLL.Concrete
{
    public class UserManager : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUserDal _userDal;
        private readonly ICompanyDal _companyDal;
        private readonly ICompanyUserDal _companyUserDal;
        private readonly IRoleService _roleService;
        private readonly IConfiguration _configuration;
        public UserManager(UserManager<User> userManager, SignInManager<User> signInManager, IUserDal userDal, ICompanyDal companyDal, ICompanyUserDal companyUserDal, IConfiguration configuration, IRoleService roleService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userDal = userDal;
            _companyDal = companyDal;
            _companyUserDal = companyUserDal;
            _configuration = configuration;
            _roleService = roleService;
        }

        public async Task<IDataResult<User>> ChangePassword(UserChangePasswordViewModel userChangePasswordViewModel, int userId)
        {
            var user = _userManager.Users.FirstOrDefault(I => I.Id == userId);
            var changePasswordResult = await _userManager.ChangePasswordAsync(user, userChangePasswordViewModel.OldPassword, userChangePasswordViewModel.Password);
            if (changePasswordResult.Succeeded)
            {
                return new SuccessDataResult<User>(user);
            }
            return new ErrorDataResult<User>("Parola değiştirilemedi.");
        }

        public async Task<IDataResult<User>> Login(UserSignInViewModel model)
        {

            var identityResult = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, true);

            if (identityResult.Succeeded)
            {
                User LoggedInUser = _userDal.Get(x => x.UserName == model.UserName);
                return new SuccessDataResult<User>(LoggedInUser);
            }
            return new ErrorDataResult<User>("Kullanıcı adı veya şifre yanlış.");

        }
        public async Task<IDataResult<User>> Register(UserSignUpViewModel model)
        {
            var company = _companyDal.Get(x => x.CompanyName == model.CompanyName);
            var user = _userDal.Get(x => x.UserName == model.UserName);

            if (user!=null)
            {
                return new ErrorDataResult<User>("Bu kullanıcı adı ekli.");
            }
            else
            {
                try
                {
                    var isAdded = await AddUser(user, company, model);
                    return new SuccessDataResult<User>("Ekleme başarılı.");
                }
                catch (Exception ex)
                {
                    return new ErrorDataResult<User>(ex.Message);

                }
            }

        }

        public async Task<IResult> SignOut()
        {
           await _signInManager.SignOutAsync();
            return new SuccessResult();
        }

        private async Task<bool> AddUser(User user, Company company, UserSignUpViewModel model)
        {
            int userId = 0, companyId = 0;
            if (user == null)
            {
                User newUser = new User()
                {
                    UserName = model.UserName
                };
                var password = _configuration.GetValue<string>("DefaultPassword");

                var result = await _userManager.CreateAsync(newUser, password);

                if (!result.Succeeded)
                {
                    throw new Exception("Kullanıcı eklenemedi.");
                }
                else
                {
                    userId = _userDal.Get(x => x.UserName == model.UserName).Id;
                }
            }
            else
            {
                userId = user.Id;
            }

            if (company == null)
            {
                Company newCompany = new Company()
                {
                    CompanyName = model.CompanyName
                };
                companyId = _companyDal.Add(newCompany);
                if (companyId == 0)
                {
                    throw new Exception("Firma eklenemedi.");
                }
            }
            else
            {
                companyId = company.Id;
            }

            if (userId != 0 && companyId != 0)
            {
                CompanyUser companyUser = new CompanyUser()
                {
                    CompanyId = companyId,
                    UserId = userId
                };
                _companyUserDal.Add(companyUser);
                await _roleService.AssignRole(new RoleAssignViewModel { Name = "User", UserId = userId });
                return true;
            }
            return false;
        }
    }
}
