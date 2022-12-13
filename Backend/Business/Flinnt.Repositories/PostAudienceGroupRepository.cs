using Flinnt.Domain;
using Flinnt.Interfaces.Repositories;

namespace Flinnt.Repositories
{
    public class PostAudienceGroupRepository : BaseRepository<PostAudienceGroup>, IPostAudienceGroupRepository
    {
        public PostAudienceGroupRepository(edplexdbContext context) : base(context)
        {
        }
    }
}