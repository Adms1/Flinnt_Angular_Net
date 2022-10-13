using Flinnt.Business.ViewModels;
using Flinnt.Domain;
using Flinnt.Interfaces.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flinnt.Interfaces.Repositories
{
    public interface IInstituteDivisionRepository : IBaseRepository<InstituteDivision>
    {
        Task<List<InstituteDivisionViewModel>> GetInstituteDivisionRecord(int instituteId);
    }
}