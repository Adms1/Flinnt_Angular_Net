using Flinnt.Domain;
using Flinnt.Interfaces.Repositories;

namespace Flinnt.Repositories
{
    public class PostPollRepository : BaseRepository<PostPoll>, IPostPollRepository
    {
        public PostPollRepository(edplexdbContext context) : base(context)
        {
        }
    }
}