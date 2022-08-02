using Microsoft.EntityFrameworkCore;

namespace Flinnt.Domain
{
    public partial class FlinntContext : DbContext
    {
        public FlinntContext()
        {
        }
        public FlinntContext(DbContextOptions<FlinntContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Account { get; set; }
    }
}