using Flinnt.Domain;
using Flinnt.Interfaces.Repositories;

namespace Flinnt.Repositories
{
    public class BoardRepository : BaseRepository<Board>, IBoardRepository
    {
        public BoardRepository(edplexdbContext context) : base(context)
        {
        }
    }
}