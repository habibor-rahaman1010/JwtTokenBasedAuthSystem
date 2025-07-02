using CustomAuthSystem.DomainEntities;
using CustomAuthSystem.UnitOfWorksInterface;
using Microsoft.AspNetCore.Identity;

namespace CustomAuthSystem.IdentitySeeder
{
    public static class UserSeeder
    {
        public static async Task SeedAdminUserAsync(IServiceProvider serviceProvider, string email, string password, string role)
        {
            var _authenticationUnitOfWork = serviceProvider.GetRequiredService<IAuthenticationUnitOfWork>();
            var _passwordHasher = serviceProvider.GetRequiredService<IPasswordHasher<ApplicationUser>>();
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger("UserSeeder");

            try
            {
                var existingUser = await _authenticationUnitOfWork.AuthenticationRepository.GetByEmailAsync(x => x.Email == email);

                if (existingUser == null)
                {
                    var adminUser = new ApplicationUser
                    {
                        UserName = email,
                        Email = email,
                        Role = role
                    };
                    adminUser.PasswordHashed = _passwordHasher.HashPassword(adminUser, password);

                    await _authenticationUnitOfWork.AuthenticationRepository.RegisterAsync(adminUser);
                    await _authenticationUnitOfWork.SaveAsync();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while seeding the admin user.");
            }
        }
    }
}
