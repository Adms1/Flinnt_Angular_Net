using AutoMapper;
using Flinnt.Business.ViewModels;
using Flinnt.Domain;
using Flinnt.Interfaces.Services;
using Flinnt.UoW;
using System;
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

        public async Task<UserAccountVerification> GetByUserIdAsync(long userId)
        {
            var result = mapper.Map<List<UserAccountVerification>>(await unitOfWork.UserAccountVerificationRepository.FindByAsync(x=>x.UserId == userId
            && !x.VerifyDateTime.HasValue));
            return result.ToList().OrderByDescending(x=>x.UserAccountVerificationId).FirstOrDefault();
        }

        public async Task<UserAccountVerification> AddAsync(UserAccountVerification model)
        {
            return await Task.FromResult(await unitOfWork.UserAccountVerificationRepository.AddAsync(model));
        }

        public async Task<bool> UpdateAsync(UserAccountVerification model)
        {
            var userAccountVerification = await unitOfWork.UserAccountVerificationRepository.GetAsync(model.UserAccountVerificationId);
            if (userAccountVerification != null)
            {
                userAccountVerification.UserId = model.UserId;
                //MAP other fields
                await unitOfWork.UserAccountVerificationRepository.UpdateAsync(userAccountVerification);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }
    }
}