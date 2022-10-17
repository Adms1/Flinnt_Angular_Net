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
    public class InstituteService : ServiceBase, IInstituteService
    {
        public InstituteService(IUnitOfWork unitOfWork, IMapper _mapper) : base(unitOfWork, _mapper)
        {
        }

        public async Task<List<InstituteModel>> GetAllAsync()
        {
            var result = mapper.Map<List<InstituteModel>>(await unitOfWork.InstituteRepository.GetAllAsync());
            return result.ToList();
        }

        public async Task<InstituteModel> GetAsync(int id)
        {
            return mapper.Map<InstituteModel>(await unitOfWork.InstituteRepository.GetAsync(id));
        }

        public async Task<InstituteModel> AddAsync(InstituteModel model)
        {
            return mapper.Map<InstituteModel>(await Task.FromResult(await unitOfWork.InstituteRepository.AddAsync(mapper.Map<InstituteModel, Institute>(model))));
        }

        public async Task<bool> UpdateAsync(InstituteViewModel model)
        {
            var institute = await unitOfWork.InstituteRepository.GetAsync(model.InstituteId);
            if (institute != null)
            {
                institute.InstituteTypeId = model.InstituteTypeId;
                institute.GroupStructureId = model.GroupStructureId;
                //MAP other fields
                await unitOfWork.InstituteRepository.UpdateAsync(institute);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var institute = unitOfWork.InstituteRepository.GetAsync(id).Result;
            if (institute != null)
            {
                await unitOfWork.InstituteRepository.DeleteAsync(institute);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }
    }
}