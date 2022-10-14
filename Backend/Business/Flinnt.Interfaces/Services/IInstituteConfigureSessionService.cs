using Flinnt.Business.ViewModels;
using Flinnt.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flinnt.Interfaces.Services
{
    public interface IInstituteConfigureSessionService : IBaseService<InstituteConfigureSession>
    {
        Task<InstituteConfigureSessionViewModel> GetAsync(int id);
        Task<bool> AddAsync(InstituteConfigureSessionViewModel model);
    }
}