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
    public class UserProfileService : ServiceBase, IUserProfileService
    {
        public UserProfileService(IUnitOfWork unitOfWork, IMapper _mapper) : base(unitOfWork, _mapper)
        {
        }

        public async Task<List<UserProfileModel>> GetAllAsync()
        {
            var result = mapper.Map<List<UserProfileModel>>(await unitOfWork.UserProfileRepository.GetAllAsync());
            return result.ToList();
        }

        public async Task<UserProfileModel> GetAsync(int id)
        {
            return mapper.Map<UserProfileModel>(await unitOfWork.UserProfileRepository.GetAsync(id));
        }

        public async Task<bool> AddAsync(UserProfile model)

        {
            await unitOfWork.UserProfileRepository.AddAsync(model);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync(UserProfile model)
        {
            var userProfile = await unitOfWork.UserProfileRepository.GetAsync(model.UserProfileId);
            if (userProfile != null)
            {
                userProfile.UserProfileId = model.UserProfileId;
                //MAP other fields
                await unitOfWork.UserProfileRepository.UpdateAsync(userProfile);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var userProfile = unitOfWork.UserProfileRepository.GetAsync(id).Result;
            if (userProfile != null)
            {
                await unitOfWork.UserProfileRepository.DeleteAsync(userProfile);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }
    }
}