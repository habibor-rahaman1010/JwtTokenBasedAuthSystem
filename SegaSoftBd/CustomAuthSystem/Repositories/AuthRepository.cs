using CustomAuthSystem.DatabaseContext;
using CustomAuthSystem.DomainEntities;
using CustomAuthSystem.RepositoriesInterafce;
using CustomAuthSystem.RequestResponseDtos.AccountDtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CustomAuthSystem.Repositories
{
    public abstract class AuthRepository<TEntity, TKey> : IAuthRepository<TEntity, TKey>
        where TEntity : class, IUser<TKey>
        where TKey : IComparable<TKey>
    {
        private readonly DbContext? _dbContext;
        private readonly DbSet<TEntity>? _dbSet;
        private readonly IPasswordHasher<TEntity> _passwordHasher;

        public AuthRepository(DbContext dbContext, IPasswordHasher<TEntity> passwordHasher)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext?.Set<TEntity>();
            _passwordHasher = passwordHasher;
        }

        public virtual async Task RegisterAsync(TEntity request, CancellationToken cancellationToken = default)
        {
            if (_dbSet == null)
            {
                throw new InvalidOperationException("DbSet is null. Cannot register entity.");
            }

            await _dbSet.AddAsync(request, cancellationToken);
        }

        public virtual async Task<TEntity> LoginAsync(UserLoginDtoRequest request, CancellationToken cancellationToken = default)
        {
            if (_dbSet == null)
            {
                throw new InvalidOperationException("User data source (DbSet) is not initialized.");
            }

            try
            {
                var user = await _dbSet.FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);
                if (user == null)
                {
                    return null;
                }

                var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHashed, request.Password);
                if (verificationResult == PasswordVerificationResult.Failed)
                {
                    return null;
                }

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while attempting to login.", ex);
            }
        }


        public virtual Task LogoutAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> AssignRoleAsync(string email, string role, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }


        public virtual async Task DeleteAsync(TEntity entityToDelete, CancellationToken cancellationToken)
        {
            if (_dbSet == null)
            {
                throw new InvalidOperationException("DbSet is not initialized.");
            }
            await Task.Run(() =>
            {
                if (_dbContext.Entry(entityToDelete).State == EntityState.Detached)
                {
                    _dbSet.Attach(entityToDelete);
                }
                _dbSet.Remove(entityToDelete);
            }, cancellationToken);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            if (_dbSet == null)
            {
                throw new InvalidOperationException("DbSet is not initialized.");
            }
            IQueryable<TEntity> query = _dbSet.AsQueryable<TEntity>();
            return await query.ToListAsync();
        }

        public virtual async Task<TEntity?> GetByEmailAsync(Expression<Func<TEntity, bool>> predicate)
        {
            if (_dbSet == null)
            {
                throw new InvalidOperationException("DbSet is not initialized.");
            }
            IQueryable<TEntity> query = _dbSet.AsQueryable<TEntity>();
            return await query.FirstOrDefaultAsync(predicate);
        }

        public virtual async Task<TEntity?> GetByIdAsync(Expression<Func<TEntity, bool>> predicate)
        {
            if (_dbSet == null)
            {
                throw new InvalidOperationException("DbSet is not initialized.");
            }
            IQueryable<TEntity> query = _dbSet.AsQueryable<TEntity>();
            return await query.FirstOrDefaultAsync(predicate);
        }

        public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter)
        {
            if (_dbSet == null)
            {
                throw new InvalidOperationException("DbSet is not initialized.");
            }
            IQueryable<TEntity> query = _dbSet.AsQueryable<TEntity>();
            return await query.AnyAsync(filter);
        }

        public virtual async Task UpdateAsync(TEntity user)
        {
            if (_dbSet == null)
            {
                throw new InvalidOperationException("DbSet is not initialized.");
            }
            await Task.Run(() =>
            {
                _dbSet.Attach(user);
                _dbContext.Entry(user).State = EntityState.Modified;
            });
        }
    }
}
