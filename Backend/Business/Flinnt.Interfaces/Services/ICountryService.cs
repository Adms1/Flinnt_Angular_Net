using Flinnt.Business.ViewModels;
using Flinnt.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flinnt.Interfaces.Services
{
    public interface ICountryService : IBaseService<Country>
    {
        Task<List<CountryViewModel>> GetAllAsync();
        Task<CountryViewModel> GetAsync(int id);
    }
}