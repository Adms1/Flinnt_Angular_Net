using Flinnt.Business.ViewModels;
using Flinnt.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flinnt.Interfaces.Services
{
    public interface IUserSettingService : IBaseService<UserSetting>
    {
        Task<UserSetting> GetAsync(int id);

        Task<UserSetting> AddAsync(UserSetting model);

        Task<bool> UpdateAsync(UserSetting model);

        Task<bool> DeleteAsync(int id);
    }
}