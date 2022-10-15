using Flinnt.Business.ViewModels;
using Flinnt.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flinnt.Interfaces.Services
{
    public interface IUserProfileService : IBaseService<UserProfile>
    {
        Task<UserProfileModel> GetByUserIdAsync(long id);
        Task<UserProfile> GetByEmailAsync(string emailId);

        Task<bool> AddAsync(UserProfile model);

        Task<bool> UpdateAsync(UserProfile model);

        Task<bool> DeleteAsync(int id);
    }
}