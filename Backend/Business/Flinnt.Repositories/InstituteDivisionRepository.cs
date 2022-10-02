using Flinnt.Domain;
using Flinnt.Interfaces.Repositories;

namespace Flinnt.Repositories
{
    public class InstituteDivisionRepository : BaseRepository<InstituteDivision>, IInstituteDivisionRepository
    {
        public InstituteDivisionRepository(edplexdbContext context) : base(context)
        {
        }
    }
}