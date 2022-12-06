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
    public class CountryService : ServiceBase, ICountryService
    {
        public CountryService(IUnitOfWork unitOfWork, IMapper _mapper) : base(unitOfWork, _mapper)
        {
        }

        public async Task<List<CountryViewModel>> GetAllAsync()
        {
            var result = mapper.Map<List<CountryViewModel>>(await unitOfWork.CountryRepository.GetAllAsync());
            return result.ToList();
        }

        public async Task<CountryViewModel> GetAsync(int id)
        {
            return mapper.Map<CountryViewModel>(await unitOfWork.CountryRepository.GetAsync(id));
        }

        public async Task<CountryViewModel> GetByCountryNameAsync(string countryName)
        {
            return mapper.Map<CountryViewModel>(await Task.FromResult(await unitOfWork.CountryRepository.FindByFirstOrDefaultAsync(x => x.CountryName.ToLower() == countryName.ToLower())));
        }
    }
}