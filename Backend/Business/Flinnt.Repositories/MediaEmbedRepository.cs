using Flinnt.Domain;
using Flinnt.Interfaces.Repositories;

namespace Flinnt.Repositories
{
    public class MediaEmbedRepository : BaseRepository<MediaEmbedService>, IMediaEmbedServiceRepository
    {
        public MediaEmbedRepository(edplexdbContext context) : base(context)
        {
        }
    }
}