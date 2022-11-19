using Flinnt.Domain;
using Flinnt.Interfaces.Repositories;

namespace Flinnt.Repositories
{
    public class UserParentChildRelationshipRepository : BaseRepository<UserParentChildRelationship>, IUserParentChildRelationshipRepository
    {
        public UserParentChildRelationshipRepository(edplexdbContext context) : base(context)
        {
        }
    }
}