using Flinnt.Domain;
using Flinnt.Interfaces.Repositories;

namespace Flinnt.Repositories
{
    public class InstituteTypeRepository : BaseRepository<InstituteType>, IInstituteTypeRepository
    {
        public InstituteTypeRepository(edplexdbContext context) : base(context)
        {
        }
    }
}