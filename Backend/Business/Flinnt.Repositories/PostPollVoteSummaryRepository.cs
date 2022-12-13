using Flinnt.Domain;
using Flinnt.Interfaces.Repositories;

namespace Flinnt.Repositories
{
    public class PostPollVoteSummaryRepository : BaseRepository<PostPollVoteSummary>, IPostPollVoteSummaryRepository
    {
        public PostPollVoteSummaryRepository(edplexdbContext context) : base(context)
        {
        }
    }
}