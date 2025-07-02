using CustomAuthSystem.DomainEntities;
using CustomAuthSystem.RepositoriesInterafce;
using CustomAuthSystem.RequestResponseDtos.AccountDtos;
using CustomAuthSystem.ServicesInterface;
using CustomAuthSystem.UnitOfWorksInterface;
using CustomAuthSystem.Utilities;

namespace CustomAuthSystem.Services
{
    public class AuthenticationManagementService : IAuthenticationManagementService
    {
        private readonly IAuthenticationUnitOfWork _authenticationUnitOfWork;
        private readonly IApplicationTime _applicationTime;
        private readonly ITokenRepository _tokenRepository;

        public AuthenticationManagementService(IAuthenticationUnitOfWork authenticationUnitOfWork,
            ITokenRepository tokenRepository,
            IApplicationTime applicationTime)
        {
            _authenticationUnitOfWork = authenticationUnitOfWork;
            _tokenRepository = tokenRepository;
            _applicationTime = applicationTime;
        }


        public async Task<ApplicationUser> RegisterUserAsync(ApplicationUser user, CancellationToken cancellationToken = default)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null.");
            }

            await _authenticationUnitOfWork.AuthenticationRepository.RegisterAsync(user, cancellationToken);
            await _authenticationUnitOfWork.SaveAsync();

            return user;
        }


        public async Task<ApplicationUser> LoginUserAsync(UserLoginDtoRequest request, CancellationToken cancellationToken = default)
        {
            return await _authenticationUnitOfWork.AuthenticationRepository.LoginAsync(request, cancellationToken);
        }



        public Task<bool> DeleteUserAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ApplicationUser>> GetAllUserAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> GetUserByIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }


        public Task UpdateUserAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UserExistAsync(string email)
        {
            return await _authenticationUnitOfWork.AuthenticationRepository.ExistsAsync(x => x.Email == email);
        }
    }
}
