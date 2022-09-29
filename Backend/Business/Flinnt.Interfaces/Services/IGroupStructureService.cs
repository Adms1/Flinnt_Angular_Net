using Flinnt.Business.ViewModels.Institute;
using Flinnt.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flinnt.Interfaces.Services
{
    public interface IGroupStructureService : IBaseService<GroupStructure>
    {
        Task<List<GroupStructureViewModel>> GetAllAsync();
    }
}