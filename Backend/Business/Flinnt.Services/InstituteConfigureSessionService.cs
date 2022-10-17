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
    public class InstituteConfigureSessionService : ServiceBase, IInstituteConfigureSessionService
    {
        public InstituteConfigureSessionService(IUnitOfWork unitOfWork, IMapper _mapper) : base(unitOfWork, _mapper)
        {
        }

        public async Task<InstituteConfigureSessionViewModel> GetAsync(int id)
        {
            return mapper.Map<InstituteConfigureSessionViewModel>(await unitOfWork.InstituteConfigureSessionRepository.FindByFirstOrDefaultAsync(x=>x.InstituteId == id));
        }

        public async Task<bool> AddAsync(InstituteConfigureSessionViewModel model)
        {
            var instituteConfigureSessions = await unitOfWork.InstituteConfigureSessionRepository.FindByAsync(x => x.InstituteId == model.InstituteId);
            if (instituteConfigureSessions.Any())
            {
                await unitOfWork.InstituteConfigureSessionRepository.DeleteAllAsync(instituteConfigureSessions);
            }

            await unitOfWork.InstituteConfigureSessionRepository.AddAsync(
                    mapper.Map<InstituteConfigureSessionViewModel, InstituteConfigureSession>(new InstituteConfigureSessionViewModel
                    {
                        BoardId = model.BoardId,
                        MediumId = model.MediumId,
                        InstituteId = model.InstituteId,
                        CurrentStep = model.CurrentStep,
                        GroupStructureId = model.GroupStructureId,
                        InstituteTypeId = model.InstituteTypeId
                    }));

            return true;
        }
    }
}