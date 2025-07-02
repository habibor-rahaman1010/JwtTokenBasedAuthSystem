using CustomAuthSystem.DomainEntities;
using CustomAuthSystem.RequestResponseDtos.AccountDtos;

namespace CustomAuthSystem.RepositoriesInterafce
{
    public interface ITokenRepository
    {
        public Task<string> CreateJwtTokenAsync(ApplicationUser user);
        public Task<string> GenerateAndSaveRefreshTokenAsync(ApplicationUser user);
        public Task<UserLoginResponseDto> RefreshTokenAsync(RefreshTokenRequestDto request);
    }
}
