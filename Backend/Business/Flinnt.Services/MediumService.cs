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
    public class MediumService : ServiceBase, IMediumService
    {
        public MediumService(IUnitOfWork unitOfWork, IMapper _mapper) : base(unitOfWork, _mapper)
        {
        }

        public async Task<List<MediumViewModel>> GetAllAsync()
        {
            var result = mapper.Map<List<MediumViewModel>>(await unitOfWork.MediumRepository.GetAllAsync());
            return result.ToList();
        }
    }
}