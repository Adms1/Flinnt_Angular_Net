using Flinnt.Business.ViewModels;
using Flinnt.Domain;
using Flinnt.Interfaces.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flinnt.Interfaces.Repositories
{
    public interface IInstituteGroupRepository : IBaseRepository<InstituteGroup>
    {
        Task<List<InstituteGroupViewModel>> GetInstituteGroupRecord(int instituteId);
    }
}