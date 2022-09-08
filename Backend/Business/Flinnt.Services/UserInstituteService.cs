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
    public class UserInstituteService : ServiceBase, IUserInstituteService
    {
        public UserInstituteService(IUnitOfWork unitOfWork, IMapper _mapper) : base(unitOfWork, _mapper)
        {
        }

        public async Task<List<UserInstitute>> GetAllAsync()
        {
            var result = mapper.Map<List<UserInstitute>>(await unitOfWork.UserInstituteRepository.GetAllAsync());
            return result.ToList();
        }

        public async Task<UserInstitute> GetAsync(int id)
        {
            return mapper.Map<UserInstitute>(await unitOfWork.UserInstituteRepository.GetAsync(id));
        }

        public async Task<UserInstitute> AddAsync(UserInstitute model)
        {
            return await Task.FromResult(await unitOfWork.UserInstituteRepository.AddAsync(model));
        }

        public async Task<bool> UpdateAsync(UserInstitute model)
        {
            var userInstitute = await unitOfWork.UserInstituteRepository.GetAsync(model.UserId);
            if (userInstitute != null)
            {
                userInstitute.UserId = model.UserId;
                //MAP other fields
                await unitOfWork.UserInstituteRepository.UpdateAsync(userInstitute);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var userInstitute = unitOfWork.UserInstituteRepository.GetAsync(id).Result;
            if (userInstitute != null)
            {
                await unitOfWork.UserInstituteRepository.DeleteAsync(userInstitute);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }
    }
}