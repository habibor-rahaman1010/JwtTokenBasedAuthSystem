using CustomAuthSystem.RepositoriesInterafce;

namespace CustomAuthSystem.UnitOfWorksInterface
{
    public interface IAuthenticationUnitOfWork : IUnitOfWork
    {
        public IAuthenticationRepository AuthenticationRepository { get; }
    }
}
