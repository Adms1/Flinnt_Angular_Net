using Flinnt.Domain;
using Flinnt.Interfaces.Repositories;

namespace Flinnt.Repositories
{
    public class PostTemplateCategoryRepository : BaseRepository<PostTemplateCategory>, IPostTemplateCategoryRepository
    {
        public PostTemplateCategoryRepository(edplexdbContext context) : base(context)
        {
        }
    }
}