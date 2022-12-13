using Flinnt.Domain;
using Flinnt.Interfaces.Repositories;

namespace Flinnt.Repositories
{
    public class PostLogRepository : BaseRepository<PostLog>, IPostLogRepository
    {
        public PostLogRepository(edplexdbContext context) : base(context)
        {
        }
    }
}