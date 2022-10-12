﻿using AutoMapper;
using Flinnt.Business.ViewModels;
using Flinnt.Domain;
using Flinnt.Interfaces.Services;
using Flinnt.UoW;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flinnt.Services
{
    public class InstituteDivisionService : ServiceBase, IInstituteDivisionService
    {
        public InstituteDivisionService(IUnitOfWork unitOfWork, IMapper _mapper) : base(unitOfWork, _mapper)
        {
        }

        public async Task<List<InstituteDivisionViewModel>> GetAllAsync()
        {
            var result = mapper.Map<List<InstituteDivisionViewModel>>(await unitOfWork.InstituteDivisionRepository.GetAllAsync());
            return result.ToList();
        }

        public async Task<InstituteDivisionViewModel> GetAsync(int id)
        {
            return mapper.Map<InstituteDivisionViewModel>(await unitOfWork.InstituteDivisionRepository.GetAsync(id));
        }

        public async Task<InstituteDivisionViewModel> GetByInstituteGroupIdAsync(int instituteGroupId)
        {
            return mapper.Map<InstituteDivisionViewModel>(await unitOfWork.InstituteDivisionRepository.FindByFirstOrDefaultAsync(x => x.InstituteGroupId == instituteGroupId));
        }

        public async Task<bool> AddAsync(InstituteDivisionViewModel model)
        {
            if (!string.IsNullOrEmpty(model.DivisionName))
            {
                int DisplayOrder = 0;
                string[] divisions = model.DivisionName.Split('\n');
                foreach (var item in divisions)
                {
                    await unitOfWork.InstituteDivisionRepository.AddAsync(
                        mapper.Map<InstituteDivisionViewModel, InstituteDivision>(new InstituteDivisionViewModel
                        {
                            DivisionName = item,
                            InstituteGroupId = model.InstituteGroupId,
                            DisplayOrder = DisplayOrder
                        }));
                    DisplayOrder++;
                }
            }
            else
                return false;
            return true;
        }

        public async Task<bool> UpdateAsync(InstituteDivisionViewModel model)
        {
            var instituteDivision = await unitOfWork.InstituteDivisionRepository.GetAsync(model.InstituteDivisionId);
            if (instituteDivision != null)
            {
                await unitOfWork.InstituteDivisionRepository.UpdateAsync(instituteDivision);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var instituteDivision = unitOfWork.InstituteDivisionRepository.GetAsync(id).Result;
            if (instituteDivision != null)
            {
                await unitOfWork.InstituteDivisionRepository.DeleteAsync(instituteDivision);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }
    }
}