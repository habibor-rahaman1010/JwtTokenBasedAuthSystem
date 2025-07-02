using CustomAuthSystem.DomainEntities;

namespace CustomAuthSystem.RepositoriesInterafce
{
    public interface IAuthenticationRepository : IAuthRepository<ApplicationUser, Guid>
    {
    }
}
