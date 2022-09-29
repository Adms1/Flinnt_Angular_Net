using Flinnt.Domain;
using Flinnt.Interfaces.Repositories;

namespace Flinnt.Repositories
{
    public class StandardRepository : BaseRepository<Standard>, IStandardRepository
    {
        public StandardRepository(edplexdbContext context) : base(context)
        {
        }
    }
}