using Flinnt.Business.ViewModels;
using Flinnt.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flinnt.Interfaces.Services
{
    public interface IUserInstituteService : IBaseService<UserInstitute>
    {
        Task<UserInstitute> GetAsync(int id);

        Task<UserInstitute> GetByUserIdAsync(long userId);
        Task<UserInstitute> AddAsync(UserInstitute model);

        Task<bool> UpdateAsync(UserInstitute model);

        Task<bool> DeleteAsync(int id);
    }
}