using CustomAuthSystem.DomainEntities;
using Microsoft.EntityFrameworkCore;

namespace CustomAuthSystem.DatabaseContext
{
    public class SegaSoftDbContext : DbContext
    {
        public SegaSoftDbContext(DbContextOptions<SegaSoftDbContext> options) : base(options)
        {
            
        }

        public DbSet<Person> Persons { get; set; }
    }
}
