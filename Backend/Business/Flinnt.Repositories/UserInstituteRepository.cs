using Flinnt.Domain;
using Flinnt.Interfaces.Repositories;

namespace Flinnt.Repositories
{
    public class UserInstituteRepository : BaseRepository<UserInstitute>, IUserInstituteRepository
    {
        public UserInstituteRepository(edplexdbContext context) : base(context)
        {
        }
    }
}