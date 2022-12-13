using Flinnt.Domain;
using Flinnt.Interfaces.Repositories;

namespace Flinnt.Repositories
{
    public class PostPollOptionRepository : BaseRepository<PostPollOption>, IPostPollOptionRepository
    {
        public PostPollOptionRepository(edplexdbContext context) : base(context)
        {
        }
    }
}