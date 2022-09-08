using Flinnt.Domain;
using Flinnt.Interfaces.Repositories;

namespace Flinnt.Repositories
{
    public class UserAccountHistoryRepository : BaseRepository<UserAccountHistory>, IUserAccountHistoryRepository
    {
        public UserAccountHistoryRepository(edplexdbContext context) : base(context)
        {
        }
    }
}