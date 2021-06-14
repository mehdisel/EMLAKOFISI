using EmlakOfisi.BLL.Abstract;
using EmlakOfisi.Core.Utilities.Results;
using EmlakOfisi.Entities.Concrete;
using EmlakOfisi.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmlakOfisi.BLL.Concrete
{
    public class RoleManager : IRoleService
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        
        public RoleManager(RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IResult> AddRole(string roleName)
        {
            var roleExists= _roleManager.Roles.Any(x => x.Name.ToLower() == roleName.ToLower());
            if (roleExists)
            {
                return new SuccessResult("Rol ekli.");
            }
            Role role = new Role
            {
                Name = roleName
            };
            var identityResult = await _roleManager.CreateAsync(role);
            if (identityResult.Succeeded)
            {
                return new SuccessResult("Ekleme başarılı");
            }
            else
            {
                StringBuilder errorMessages = new StringBuilder();
                foreach (var item in identityResult.Errors)
                {
                    errorMessages.Append(item.Description + "\n");
                }
                return new ErrorResult("Ekleme başarısız. Hata:" + errorMessages.ToString());
            }
        }

        public async Task<IResult> AssignRole(RoleAssignViewModel roleAssignViewModel)
        {

            var user = _userManager.Users.FirstOrDefault(I => I.Id == roleAssignViewModel.UserId);
            if (user==null)
            {
                return new ErrorResult("User bulunamadi");
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            if (!userRoles.Any(x=>x!= roleAssignViewModel.Name))
            {
                var assignedRole = await _userManager.AddToRoleAsync(user, roleAssignViewModel.Name);
                if (assignedRole.Succeeded)
                {
                    return new SuccessResult("Yetkilendirme başarılı");
                }
                else
                {
                    StringBuilder errorMessages = new StringBuilder();
                    foreach (var item in assignedRole.Errors)
                    {
                        errorMessages.Append(item.Description + "\n");
                    }
                    return new ErrorResult("Yetkilendirme başarısız. Hata:" + errorMessages.ToString());
                }
            }
            else
            {
                return new SuccessResult("Yetkilendirme mevcut.");
            }


           

        }
    }
}
