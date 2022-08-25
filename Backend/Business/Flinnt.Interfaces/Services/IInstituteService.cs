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

        Task<bool> AddAsync(Institute model);

        Task<bool> UpdateAsync(Institute model);

        Task<bool> DeleteAsync(int id);
    }
}