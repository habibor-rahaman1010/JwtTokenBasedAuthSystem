using CustomAuthSystem.DomainEntities;
using CustomAuthSystem.RepositoriesInterafce;
using CustomAuthSystem.RequestResponseDtos.AccountDtos;
using CustomAuthSystem.UnitOfWorksInterface;
using CustomAuthSystem.Utilities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CustomAuthSystem.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IApplicationTime _applicationTime;

        private readonly IAuthenticationUnitOfWork _authenticationUnitOfWork;

        public TokenRepository(IConfiguration configuration, IApplicationTime applicationTime,
            IAuthenticationUnitOfWork authenticationUnitOfWork)
        {
            _authenticationUnitOfWork = authenticationUnitOfWork;
            _configuration = configuration;
            _applicationTime = applicationTime;
        }

        public async Task<string> CreateJwtTokenAsync(ApplicationUser user)
        {
            return await Task<string>.Run(() => {

                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                        issuer: _configuration["Jwt:Issuer"],
                        audience: _configuration["Jwt:Audience"],
                        claims: claims,
                        expires: _applicationTime.GetCurrentTime().AddMinutes(1),
                        signingCredentials: credentials
                    );

                return new JwtSecurityTokenHandler().WriteToken(token);
            });
        }


        public async Task<string> GenerateAndSaveRefreshTokenAsync(ApplicationUser user)
        {
            var refreshToken = await GenerateRefreshTokenAsync();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = _applicationTime.GetCurrentTime().AddDays(15);
            await _authenticationUnitOfWork.AuthenticationRepository.UpdateAsync(user);
            await _authenticationUnitOfWork.SaveAsync();
            return refreshToken;
        }


        public async Task<UserLoginResponseDto> RefreshTokenAsync(RefreshTokenRequestDto request)
        {
            var user = await ValidateRefreshTokenAsync(request.UserId, request.RefreshToken);
            if (user == null) return null;
            return await CreateResponseTokenAsync(user);
        }

        private async Task<string> GenerateRefreshTokenAsync()
        {
            return await Task<string>.Run(() =>
            {
                var randomNumber = new byte[32];
                using var rng = RandomNumberGenerator.Create();
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            });
        }

        private async Task<ApplicationUser> ValidateRefreshTokenAsync(Guid userId, string refreshToken)
        {
            var user = await _authenticationUnitOfWork.AuthenticationRepository.GetByIdAsync(x => x.Id == userId);
            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= _applicationTime.GetCurrentTime())
            {
                return null;
            }
            return user;
        }

        private async Task<UserLoginResponseDto> CreateResponseTokenAsync(ApplicationUser user)
        {
            return new UserLoginResponseDto
            {
                AccessToken = await CreateJwtTokenAsync(user),
                RefreshToken = await GenerateAndSaveRefreshTokenAsync(user),
                Email = user.Email
            };
        }
    }
}
