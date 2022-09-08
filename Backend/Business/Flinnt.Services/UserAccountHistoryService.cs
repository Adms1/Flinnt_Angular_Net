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
    public class UserAccountHistoryService : ServiceBase, IUserAccountHistoryService
    {
        public UserAccountHistoryService(IUnitOfWork unitOfWork, IMapper _mapper) : base(unitOfWork, _mapper)
        {
        }

        public async Task<List<UserAccountHistory>> GetAllAsync()
        {
            var result = mapper.Map<List<UserAccountHistory>>(await unitOfWork.UserAccountHistoryRepository.GetAllAsync());
            return result.ToList();
        }

        public async Task<UserAccountHistory> GetAsync(int id)
        {
            return mapper.Map<UserAccountHistory>(await unitOfWork.UserRepository.GetAsync(id));
        }

        public async Task<UserAccountHistory> AddAsync(UserAccountHistory model)
        {
            return await Task.FromResult(await unitOfWork.UserAccountHistoryRepository.AddAsync(model));
        }

        public async Task<bool> UpdateAsync(UserAccountHistory model)
        {
            var userAccountHistory = await unitOfWork.UserAccountHistoryRepository.GetAsync(model.UserId.Value);
            if (userAccountHistory != null)
            {
                userAccountHistory.UserId = model.UserId;
                //MAP other fields
                await unitOfWork.UserAccountHistoryRepository.UpdateAsync(userAccountHistory);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var userAccountHistory = unitOfWork.UserAccountHistoryRepository.GetAsync(id).Result;
            if (userAccountHistory != null)
            {
                await unitOfWork.UserAccountHistoryRepository.DeleteAsync(userAccountHistory);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }
    }
}