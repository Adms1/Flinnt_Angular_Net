using AutoMapper;
using Flinnt.Business.ViewModels;
using Flinnt.Domain;
using Flinnt.Interfaces.Services;
using Flinnt.UoW;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flinnt.Services
{
    public class UserSettingService : ServiceBase, IUserSettingService
    {
        public UserSettingService(IUnitOfWork unitOfWork, IMapper _mapper) : base(unitOfWork, _mapper)
        {
        }

        public async Task<List<UserSetting>> GetAllAsync()
        {
            var result = mapper.Map<List<UserSetting>>(await unitOfWork.UserSettingRepository.GetAllAsync());
            return result.ToList();
        }

        public async Task<UserSetting> GetAsync(int id)
        {
            return mapper.Map<UserSetting>(await unitOfWork.UserSettingRepository.GetAsync(id));
        }

        public async Task<UserSetting> AddAsync(UserSetting model)
        {
            return await Task.FromResult(await unitOfWork.UserSettingRepository.AddAsync(model));
        }

        public async Task<bool> UpdateAsync(UserSetting model)
        {
            var userSetting = await unitOfWork.UserSettingRepository.GetAsync(model.UserId);
            if (userSetting != null)
            {
                userSetting.UserId = model.UserId;
                //MAP other fields
                await unitOfWork.UserSettingRepository.UpdateAsync(userSetting);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var userSetting = unitOfWork.UserSettingRepository.GetAsync(id).Result;
            if (userSetting != null)
            {
                await unitOfWork.UserSettingRepository.DeleteAsync(userSetting);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }
    }
}