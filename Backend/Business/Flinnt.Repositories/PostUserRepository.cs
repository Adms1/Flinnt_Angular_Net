using Flinnt.Domain;
using Flinnt.Interfaces.Repositories;

namespace Flinnt.Repositories
{
    public class PostUserRepository : BaseRepository<PostUser>, IPostUserRepository
    {
        public PostUserRepository(edplexdbContext context) : base(context)
        {
        }
    }
}