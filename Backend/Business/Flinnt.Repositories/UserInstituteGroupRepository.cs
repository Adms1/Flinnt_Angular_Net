using Flinnt.Domain;
using Flinnt.Interfaces.Repositories;

namespace Flinnt.Repositories
{
    public class UserInstituteGroupRepository : BaseRepository<UserInstituteGroup>, IUserInstituteGroupRepository
    {
        public UserInstituteGroupRepository(edplexdbContext context) : base(context)
        {
        }
    }
}