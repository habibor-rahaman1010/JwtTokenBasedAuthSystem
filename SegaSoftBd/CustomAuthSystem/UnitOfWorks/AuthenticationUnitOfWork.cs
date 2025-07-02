using CustomAuthSystem.DatabaseContext;
using CustomAuthSystem.RepositoriesInterafce;
using CustomAuthSystem.UnitOfWorksInterface;

namespace CustomAuthSystem.UnitOfWorks
{
    public class AuthenticationUnitOfWork : UnitOfWork, IAuthenticationUnitOfWork
    {
        private readonly AuthenticationDbContext _dbContext;
        public IAuthenticationRepository AuthenticationRepository { get; private set; }

        public AuthenticationUnitOfWork(AuthenticationDbContext dbContext, 
            IAuthenticationRepository authenticationRepository) : base(dbContext)
        {
            _dbContext = dbContext;
            AuthenticationRepository = authenticationRepository;
        }

    }
}
