using Flinnt.Domain;
using Flinnt.Interfaces.Repositories;

namespace Flinnt.Repositories
{
    public class StudentRepository : BaseRepository<City>, IStudentRepository
    {
        public StudentRepository(edplexdbContext context) : base(context)
        {
        }
    }
}