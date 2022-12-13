using Flinnt.Domain;
using Flinnt.Interfaces.Repositories;

namespace Flinnt.Repositories
{
    public class PostMediaRepository : BaseRepository<PostMedium>, IPostMediaRepository
    {
        public PostMediaRepository(edplexdbContext context) : base(context)
        {
        }
    }
}