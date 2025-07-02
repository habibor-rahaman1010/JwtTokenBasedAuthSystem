using CustomAuthSystem.IdentitySeeder;

namespace CustomAuthSystem.ExtensionMethods
{
    public static class StartupExtensions
    {
        public static async Task SeedInitialDataAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var serviceProvider = scope.ServiceProvider;

            try
            {
                await UserSeeder.SeedAdminUserAsync(serviceProvider, "user.admin@gmail.com", "admin123", "Admin");
                await UserSeeder.SeedAdminUserAsync(serviceProvider, "user.support@gmail.com", "support123", "Support");
            }
            catch (Exception ex)
            {
                var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "Seeding failed.");
            }
        }
    }
}
