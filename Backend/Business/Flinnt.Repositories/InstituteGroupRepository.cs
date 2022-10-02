using Flinnt.Domain;
using Flinnt.Interfaces.Repositories;

namespace Flinnt.Repositories
{
    public class InstituteGroupRepository : BaseRepository<InstituteGroup>, IInstituteGroupRepository
    {
        public InstituteGroupRepository(edplexdbContext context) : base(context)
        {
        }
    }
}