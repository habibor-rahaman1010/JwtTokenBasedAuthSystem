using AutoMapper;
using CustomAuthSystem.CustomActionFilters;
using CustomAuthSystem.DomainEntities;
using CustomAuthSystem.RepositoriesInterafce;
using CustomAuthSystem.RequestResponseDtos.AccountDtos;
using CustomAuthSystem.ServicesInterface;
using CustomAuthSystem.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CustomAuthSystem.Controllers.Account
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAuthenticationController : ApiBaseController
    {
        private readonly IAuthenticationManagementService _authenticationManagementService;
        private readonly IApplicationTime _applicationTime;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
        private readonly ITokenRepository _tokenRepository;
        private readonly IMapper _mapper;

        public UserAuthenticationController(IAuthenticationManagementService authenticationManagementService,
            ITokenRepository tokenRepository,
            IApplicationTime applicationTime,
            IPasswordHasher<ApplicationUser> passwordHasher,
            IMapper mapper)
        {
            _authenticationManagementService = authenticationManagementService;
            _tokenRepository = tokenRepository;
            _passwordHasher = passwordHasher;
            _applicationTime = applicationTime;
            _mapper = mapper;
        }

        [HttpPost("Registration")]
        [ValidateModel]
        public async Task<IActionResult> Registration(UserRegistrationDtoRequest request, CancellationToken cancellationToken = default)
        {
            var existUser = await _authenticationManagementService.UserExistAsync(request.Email);
            if (existUser == true)
            {
                return BadRequest(new { message = $"User already exist by this email: {request.Email}" });
            }

            var user = _mapper.Map<ApplicationUser>(request);
            user.PasswordHashed = _passwordHasher.HashPassword(user, request.Password);
            user.CreatedDate = _applicationTime.GetCurrentTime();
            user.LastModifiedDate = _applicationTime.GetCurrentTime();


            var applicationUser = await _authenticationManagementService.RegisterUserAsync(user, cancellationToken);
            return Ok(applicationUser);
        }

        [HttpPost("login")]
        [ValidateModel]
        public async Task<ActionResult<UserLoginResponseDto>> Login(UserLoginDtoRequest request)
        {
            var user = await _authenticationManagementService.LoginUserAsync(request);
            if (user == null)
            {
                return Unauthorized(new
                {
                    message = "Invalid user email or password!"
                });
            }

            var token = await _tokenRepository.CreateJwtTokenAsync(user);
            
            return Ok(new
            {
                Message = "User logedin successfully!",
                Email = user.Email,
                JwtToken = token,
                RefreshToken = await _tokenRepository.GenerateAndSaveRefreshTokenAsync(user),
            });
        }

        [HttpPost("RefreshToken")]
        public async Task<ActionResult<UserLoginResponseDto>> RefreshToken(RefreshTokenRequestDto request)
        {
            var result = await _tokenRepository.RefreshTokenAsync(request);
            if (result == null || result.AccessToken == null || result.RefreshToken == null || result.Email == null)
            {
                return Unauthorized("Invalid Refresh Token");
            }
            return Ok(result);
        }
    }
}
