using Flinnt.Business.ViewModels;
using Flinnt.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flinnt.Interfaces.Services
{
    public interface IInstituteService : IBaseService<Institute>
    {
        Task<List<InstituteModel>> GetAllAsync();

        Task<InstituteModel> GetAsync(int id);

        Task<InstituteModel> AddAsync(InstituteModel model);

        Task<bool> UpdateAsync(InstituteModel model);

        Task<bool> DeleteAsync(int id);
    }
}