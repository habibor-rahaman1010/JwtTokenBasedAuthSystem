using CustomAuthSystem.DomainEntities;
using CustomAuthSystem.RequestResponseDtos.AccountDtos;

namespace CustomAuthSystem.ServicesInterface
{
    public interface IAuthenticationManagementService
    {

        public Task<ApplicationUser> RegisterUserAsync(ApplicationUser user, CancellationToken cancellationToken = default);
        public Task<ApplicationUser> LoginUserAsync(UserLoginDtoRequest request, CancellationToken cancellationToken = default);

        //public Task<UserLoginResponseDto> RefreshTokenAsync(RefreshTokenRequestDto request);

        public Task<IEnumerable<ApplicationUser>> GetAllUserAsync();
        public Task<ApplicationUser> GetUserByIdAsync(Guid userId);
        public Task<ApplicationUser> GetUserByEmailAsync(string email);
        public Task UpdateUserAsync(ApplicationUser user);
        public Task<bool> DeleteUserAsync(Guid userId);
        public Task<bool> UserExistAsync(string email);
    }
}
