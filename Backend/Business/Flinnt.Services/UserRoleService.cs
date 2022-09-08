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
    public class UserRoleService : ServiceBase, IUserRoleService
    {
        public UserRoleService(IUnitOfWork unitOfWork, IMapper _mapper) : base(unitOfWork, _mapper)
        {
        }

        public async Task<List<UserRole>> GetAllAsync()
        {
            var result = mapper.Map<List<UserRole>>(await unitOfWork.UserRoleRepository.GetAllAsync());
            return result.ToList();
        }

        public async Task<UserRole> GetAsync(int id)
        {
            return mapper.Map<UserRole>(await unitOfWork.UserRoleRepository.GetAsync(id));
        }

        public async Task<UserRole> AddAsync(UserRole model)
        {
            return await Task.FromResult(await unitOfWork.UserRoleRepository.AddAsync(model));
        }

        public async Task<bool> UpdateAsync(UserRole model)
        {
            var userRole = await unitOfWork.UserRoleRepository.GetAsync(model.UserId);
            if (userRole != null)
            {
                userRole.UserId = model.UserId;
                //MAP other fields
                await unitOfWork.UserRoleRepository.UpdateAsync(userRole);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var userRole = unitOfWork.UserRoleRepository.GetAsync(id).Result;
            if (userRole != null)
            {
                await unitOfWork.UserRoleRepository.DeleteAsync(userRole);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }
    }
}