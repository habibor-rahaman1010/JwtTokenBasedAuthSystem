using CustomAuthSystem.RepositoriesInterafce;

namespace CustomAuthSystem.UnitOfWorksInterface
{
    public interface ISegaSoftBbUnitOfWork : IUnitOfWork
    {
        public IPersonRepository PersonRepository { get; }
    }
}
