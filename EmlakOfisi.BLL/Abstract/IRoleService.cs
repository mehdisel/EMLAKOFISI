using EmlakOfisi.Core.Utilities.Results;
using EmlakOfisi.Models;
using System.Threading.Tasks;

namespace EmlakOfisi.BLL.Abstract
{
    public interface IRoleService
    {
        Task<IResult> AddRole(string roleName);
        Task<IResult> AssignRole(RoleAssignViewModel roleAssignViewModel);
    }
}
