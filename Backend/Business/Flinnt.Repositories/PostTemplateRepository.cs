using Flinnt.Domain;
using Flinnt.Interfaces.Repositories;

namespace Flinnt.Repositories
{
    public class PostTemplateRepository : BaseRepository<PostTemplate>, IPostTemplateRepository
    {
        public PostTemplateRepository(edplexdbContext context) : base(context)
        {
        }
    }
}