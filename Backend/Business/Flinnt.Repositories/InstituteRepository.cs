using Flinnt.Domain;
using Flinnt.Interfaces.Repositories;

namespace Flinnt.Repositories
{
    public class InstituteRepository : BaseRepository<Institute>, IInstituteRepository
    {
        public InstituteRepository(edplexdbContext context) : base(context)
        {
        }
    }
}