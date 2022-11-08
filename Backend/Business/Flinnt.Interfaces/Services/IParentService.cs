using Flinnt.Business.ViewModels;
using Flinnt.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flinnt.Interfaces.Services
{
    public interface IParentService : IBaseService<Parent>
    {
        Task<List<ParentViewModel>> GetAllAsync();
        Task<ParentViewModel> GetAsync(int id);
        Task<ParentViewModel> AddAsync(ParentViewModel model);
    }
}