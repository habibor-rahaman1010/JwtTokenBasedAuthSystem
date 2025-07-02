using CustomAuthSystem.DomainEntities;
using CustomAuthSystem.RequestResponseDtos.AccountDtos;
using System.Linq.Expressions;

namespace CustomAuthSystem.RepositoriesInterafce
{
    public interface IAuthRepository<TEntity, TKey>
        where TEntity : class, IUser<TKey>
        where TKey : IComparable<TKey>
    {
        public Task RegisterAsync(TEntity request, CancellationToken cancellationToken = default);
        public Task<TEntity> LoginAsync(UserLoginDtoRequest request, CancellationToken cancellationToken = default);
        public Task LogoutAsync(CancellationToken cancellationToken = default);
        public Task<bool> AssignRoleAsync(string email, string role, CancellationToken cancellationToken = default);


        public Task<IEnumerable<TEntity>> GetAllAsync();
        public Task<TEntity?> GetByIdAsync(Expression<Func<TEntity, bool>> predicate);
        public Task<TEntity?> GetByEmailAsync(Expression<Func<TEntity, bool>> predicate);
        public Task UpdateAsync(TEntity user);
        public Task DeleteAsync(TEntity user, CancellationToken cancellationToken = default);
        public Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter);
    }
}
