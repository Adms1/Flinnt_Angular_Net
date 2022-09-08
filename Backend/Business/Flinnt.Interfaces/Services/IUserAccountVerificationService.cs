using Flinnt.Business.ViewModels;
using Flinnt.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flinnt.Interfaces.Services
{
    public interface IUserAccountVerificationService : IBaseService<UserAccountVerification>
    {
        Task<UserAccountVerification> GetAsync(int id);

        Task<UserAccountVerification> AddAsync(UserAccountVerification model);

        Task<bool> UpdateAsync(UserAccountVerification model);

        Task<bool> DeleteAsync(int id);
    }
}