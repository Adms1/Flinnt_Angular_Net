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
    public class InstituteTypeService : ServiceBase, IInstituteTypeService
    {
        public InstituteTypeService(IUnitOfWork unitOfWork, IMapper _mapper) : base(unitOfWork, _mapper)
        {
        }

        public async Task<List<InstituteTypeViewModel>> GetAllAsync()
        {
            var result = mapper.Map<List<InstituteTypeViewModel>>(await unitOfWork.InstituteTypeRepository.GetAllAsync());
            return result.ToList();
        }
    }
}