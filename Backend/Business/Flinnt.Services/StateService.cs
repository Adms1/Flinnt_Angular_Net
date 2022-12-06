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
    public class StateService : ServiceBase, IStateService
    {
        public StateService(IUnitOfWork unitOfWork, IMapper _mapper) : base(unitOfWork, _mapper)
        {
        }

        public async Task<List<StateViewModel>> GetAllAsync()
        {
            var result = mapper.Map<List<StateViewModel>>(await unitOfWork.StateRepository.GetAllAsync());
            return result.ToList();
        }

        public async Task<StateViewModel> GetAsync(int id)
        {
            return mapper.Map<StateViewModel>(await unitOfWork.StateRepository.GetAsync(id));
        }

        public async Task<List<StateViewModel>> GetByCountryIdAsync(int CountryId)
        {
            return mapper.Map<List<StateViewModel>>(await unitOfWork.StateRepository.FindByAsync(x=>x.CountryId == CountryId));
        }

        public async Task<StateViewModel> GetByStateNameAsync(string stateName)
        {
            return mapper.Map<StateViewModel>(await Task.FromResult(await unitOfWork.StateRepository.FindByFirstOrDefaultAsync(x => x.StateName.ToLower() == stateName.ToLower())));
        }
    }
}