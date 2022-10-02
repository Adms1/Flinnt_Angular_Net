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
    public class InstituteGroupService : ServiceBase, IInstituteGroupService
    {
        public InstituteGroupService(IUnitOfWork unitOfWork, IMapper _mapper) : base(unitOfWork, _mapper)
        {
        }

        public async Task<List<InstituteGroupViewModel>> GetAllAsync()
        {
            var result = mapper.Map<List<InstituteGroupViewModel>>(await unitOfWork.InstituteGroupRepository.GetAllAsync());
            return result.ToList();
        }

        public async Task<InstituteGroupViewModel> GetAsync(int id)
        {
            return mapper.Map<InstituteGroupViewModel>(await unitOfWork.InstituteGroupRepository.GetAsync(id));
        }

        public async Task<List<InstituteGroupViewModel>> GetByInstituteIdAsync(int instituteId)
        {
            return mapper.Map<List<InstituteGroupViewModel>>(await unitOfWork.InstituteGroupRepository.FindByAsync(x=>x.InstituteId == instituteId));
        }

        public async Task<InstituteGroupViewModel> AddAsync(InstituteGroupViewModel model)
        {
            return mapper.Map<InstituteGroupViewModel>(await Task.FromResult(await unitOfWork.InstituteGroupRepository.AddAsync(mapper.Map<InstituteGroupViewModel, InstituteGroup>(model))));
        }

        public async Task<bool> UpdateAsync(InstituteGroupViewModel model)
        {
            var institute = await unitOfWork.InstituteGroupRepository.GetAsync(model.InstituteGroupId);
            if (institute != null)
            {
                await unitOfWork.InstituteGroupRepository.UpdateAsync(institute);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }
    }
}