using Flinnt.Domain;
using Flinnt.Interfaces.Repositories;

namespace Flinnt.Repositories
{
    public class InstituteConfigureSessionRepository : BaseRepository<InstituteConfigureSession>, IInstituteConfigureSessionRepository
    {
        public InstituteConfigureSessionRepository(edplexdbContext context) : base(context)
        {
        }
    }
}