using Flinnt.Business.ViewModels;
using Flinnt.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flinnt.Interfaces.Services
{
    public interface IUserAccountVerificationService : IBaseService<UserAccountVerification>
    {
        Task<UserAccountVerification> GetByUserIdAsync(long userId);

        Task<UserAccountVerification> AddAsync(UserAccountVerification model);

        Task<bool> UpdateAsync(UserAccountVerification model);
    }
}