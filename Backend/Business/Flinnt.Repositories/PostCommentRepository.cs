using Flinnt.Domain;
using Flinnt.Interfaces.Repositories;

namespace Flinnt.Repositories
{
    public class PostCommentRepository : BaseRepository<PostComment>, IPostCommentRepository
    {
        public PostCommentRepository(edplexdbContext context) : base(context)
        {
        }
    }
}