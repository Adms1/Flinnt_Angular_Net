using Flinnt.Business.ViewModels;
using Flinnt.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flinnt.Interfaces.Services
{
    public interface IStateService : IBaseService<State>
    {
        Task<List<StateViewModel>> GetAllAsync();
        Task<StateViewModel> GetAsync(int id);
        Task<List<StateViewModel>> GetByCountryIdAsync(int CountryId);
    }
}