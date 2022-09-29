using Flinnt.Domain;
using Flinnt.Interfaces.Repositories;

namespace Flinnt.Repositories
{
    public class GroupStructureRepository : BaseRepository<GroupStructure>, IGroupStructureRepository
    {
        public GroupStructureRepository(edplexdbContext context) : base(context)
        {
        }
    }
}