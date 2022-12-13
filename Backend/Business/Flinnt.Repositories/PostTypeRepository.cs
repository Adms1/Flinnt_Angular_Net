using Flinnt.Domain;
using Flinnt.Interfaces.Repositories;

namespace Flinnt.Repositories
{
    public class PostTypeRepository : BaseRepository<PostType>, IPostTypeRepository
    {
        public PostTypeRepository(edplexdbContext context) : base(context)
        {
        }
    }
}