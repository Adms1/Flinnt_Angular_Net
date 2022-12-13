using Flinnt.Domain;
using Flinnt.Interfaces.Repositories;

namespace Flinnt.Repositories
{
    public class MediaTypeRepository : BaseRepository<MediaType>, IMediaTypeRepository
    {
        public MediaTypeRepository(edplexdbContext context) : base(context)
        {
        }
    }
}