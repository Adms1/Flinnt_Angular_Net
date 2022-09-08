using Flinnt.Domain;
using Flinnt.Interfaces.Repositories;

namespace Flinnt.Repositories
{
    public class UserAccountVerificationRepository : BaseRepository<UserAccountVerification>, IUserAccountVerificationRepository
    {
        public UserAccountVerificationRepository(edplexdbContext context) : base(context)
        {
        }
    }
}