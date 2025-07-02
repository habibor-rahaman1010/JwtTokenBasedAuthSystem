using CustomAuthSystem.DomainEntities;
using Microsoft.EntityFrameworkCore;

namespace CustomAuthSystem.DatabaseContext
{
    public class AuthenticationDbContext : DbContext
    {
        public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options) : base(options)
        {
            
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}
