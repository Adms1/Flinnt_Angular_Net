using Flinnt.Business.ViewModels;
using Flinnt.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flinnt.Interfaces.Services
{
    public interface IInstituteGroupService : IBaseService<InstituteGroup>
    {
        Task<List<InstituteGroupViewModel>> GetAllAsync();
        Task<InstituteGroupViewModel> GetAsync(int id);
        Task<List<InstituteGroupViewModel>> GetByInstituteIdAsync(int instituteId);
        Task<bool> AddAsync(InstituteGroupViewModel model);
        Task<bool> UpdateAsync(InstituteGroupViewModel model);
    }
}