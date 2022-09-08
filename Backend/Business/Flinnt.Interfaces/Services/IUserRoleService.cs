using Flinnt.Business.ViewModels;
using Flinnt.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flinnt.Interfaces.Services
{
    public interface IUserRoleService : IBaseService<UserRole>
    {
        Task<UserRole> GetAsync(int id);

        Task<UserRole> AddAsync(UserRole model);

        Task<bool> UpdateAsync(UserRole model);

        Task<bool> DeleteAsync(int id);
    }
}