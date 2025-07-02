using CustomAuthSystem.DatabaseContext;
using CustomAuthSystem.DomainEntities;
using CustomAuthSystem.RepositoriesInterafce;
using Microsoft.AspNetCore.Identity;

namespace CustomAuthSystem.Repositories
{
    public class AuthenticationRepository : AuthRepository<ApplicationUser, Guid>, IAuthenticationRepository
    {
        private readonly AuthenticationDbContext _authenticationDbContext;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;

        public AuthenticationRepository(AuthenticationDbContext dbContext,
            IPasswordHasher<ApplicationUser> passwordHasher) : base(dbContext, passwordHasher)
        {
            _authenticationDbContext = dbContext;
            _passwordHasher = passwordHasher;
        }
    }
}
