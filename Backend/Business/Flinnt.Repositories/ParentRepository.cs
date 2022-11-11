using Flinnt.Domain;
using Flinnt.Interfaces.Repositories;

namespace Flinnt.Repositories
{
    public class ParentRepository : BaseRepository<Parent>, IParentRepository
    {
        public ParentRepository(edplexdbContext context) : base(context)
        {
        }
    }
}