using Flinnt.Domain;
using Flinnt.Interfaces.Repositories;

namespace Flinnt.Repositories
{
    public class PostPollVoteRepository : BaseRepository<PostPollVote>, IPostPollVoteRepository
    {
        public PostPollVoteRepository(edplexdbContext context) : base(context)
        {
        }
    }
}