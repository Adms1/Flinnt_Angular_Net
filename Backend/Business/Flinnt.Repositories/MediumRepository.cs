using Flinnt.Domain;
using Flinnt.Interfaces.Repositories;

namespace Flinnt.Repositories
{
    public class MediumRepository : BaseRepository<Medium>, IMediumRepository
    {
        public MediumRepository(edplexdbContext context) : base(context)
        {
        }
    }
}