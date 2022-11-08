using Flinnt.Business.ViewModels;
using Flinnt.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flinnt.Interfaces.Services
{
    public interface IParentService : IBaseService<City>
    {
        Task<List<CityViewModel>> GetAllAsync();
        Task<CityViewModel> GetAsync(int id);
        Task<CityViewModel> AddAsync(CityViewModel model);
    }
}