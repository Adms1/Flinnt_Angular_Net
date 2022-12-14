using Flinnt.Business.ViewModels;
using Flinnt.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flinnt.Interfaces.Services
{
    public interface IInstituteDivisionService : IBaseService<InstituteDivision>
    {
        Task<List<InstituteDivisionViewModel>> GetAllAsync();
        Task<InstituteDivisionViewModel> GetAsync(int id);
        Task<List<InstituteDivisionViewModel>> GetDivisionByInstituteIdAsync(int instituteId);
        Task<bool> AddAsync(InstituteDivisionViewModel model);
        Task<bool> UpdateAsync(InstituteDivisionViewModel model);
        Task<bool> DeleteAsync(int id);
    }
}