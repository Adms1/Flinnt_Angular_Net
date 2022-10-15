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
            return await Task.FromResult(await unitOfWork.InstituteGroupRepository.GetInstituteGroupRecord(instituteId));
        }

        public async Task<bool> AddAsync(InstituteGroupViewModel model)
        {
            var instituteGroups = await unitOfWork.InstituteGroupRepository.FindByAsync(x => x.InstituteId == model.InstituteId);
            if (instituteGroups.Any())
            {
                var instituteDivision = await unitOfWork.InstituteDivisionRepository.FindByAsync(x => instituteGroups.Select(x => x.InstituteGroupId).Contains(x.InstituteGroupId));
                if (instituteDivision.Any())
                {
                    await unitOfWork.InstituteDivisionRepository.DeleteAllAsync(instituteDivision);
                }
                await unitOfWork.InstituteGroupRepository.DeleteAllAsync(instituteGroups);
            }

            int DisplayOrder = 0;
            foreach (var item in model.Standards)
            {
                await unitOfWork.InstituteGroupRepository.AddAsync(
                    mapper.Map<InstituteGroupViewModel, InstituteGroup>(new InstituteGroupViewModel
                    {
                        StandardId = item.StandardId,
                        BoardId = model.BoardId,
                        MediumId = model.MediumId,
                        InstituteId = model.InstituteId,
                        DisplayOrder = DisplayOrder,
                        CreateDateTime = DateTime.Now
                    }));
                DisplayOrder++;
            }

            return true;
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