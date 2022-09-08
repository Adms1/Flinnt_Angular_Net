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
    public class UserAccountVerificationService : ServiceBase, IUserAccountVerificationService
    {
        public UserAccountVerificationService(IUnitOfWork unitOfWork, IMapper _mapper) : base(unitOfWork, _mapper)
        {
        }

        public async Task<List<UserAccountVerification>> GetAllAsync()
        {
            var result = mapper.Map<List<UserAccountVerification>>(await unitOfWork.UserAccountVerificationRepository.GetAllAsync());
            return result.ToList();
        }

        public async Task<UserAccountVerification> GetAsync(int id)
        {
            return mapper.Map<UserAccountVerification>(await unitOfWork.UserAccountVerificationRepository.GetAsync(id));
        }

        public async Task<UserAccountVerification> AddAsync(UserAccountVerification model)
        {
            return await Task.FromResult(await unitOfWork.UserAccountVerificationRepository.AddAsync(model));
        }

        public async Task<bool> UpdateAsync(UserAccountVerification model)
        {
            var userAccountVerification = await unitOfWork.UserAccountVerificationRepository.GetAsync(model.UserId);
            if (userAccountVerification != null)
            {
                userAccountVerification.UserId = model.UserId;
                //MAP other fields
                await unitOfWork.UserAccountVerificationRepository.UpdateAsync(userAccountVerification);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var userAccountVerification = unitOfWork.UserAccountVerificationRepository.GetAsync(id).Result;
            if (userAccountVerification != null)
            {
                await unitOfWork.UserAccountVerificationRepository.DeleteAsync(userAccountVerification);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }
    }
}