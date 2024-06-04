using FilmoSearchPortal.Domain.Models;
using FilmoSearchPortal.Infrastructure.DbContexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FilmoSearchPortal.WebApi.Middlewares
{
    public class InitializeDatabaseMiddleware
    {
        private readonly RequestDelegate next;
        private static bool isInitialized = false;
        private readonly IConfiguration _configuration;

        private static object locker = new();

        public InitializeDatabaseMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            this.next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
        {
            if (!isInitialized)
            {
                lock (locker)
                {
                    if (!isInitialized)
                    {
                        InitializeDatabase(serviceProvider);
                        CreateDefaultUser(serviceProvider).Wait();
                        CreateDefaultAdmin(serviceProvider).Wait();

                        isInitialized = true;
                    }
                }
            }

            await next.Invoke(context);
        }

        private void InitializeDatabase(IServiceProvider serviceProvider)
        {
            serviceProvider.GetRequiredService<ApplicationDbContext>().Database.Migrate();
        }

        private async Task CreateDefaultUser(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            var userConfiguration = _configuration.GetSection("AdministrationConfiguration").GetSection("User");

            var user = new User
            {
                FirstName = userConfiguration.GetSection("FirstName").Value,
                LastName = userConfiguration.GetSection("LastName").Value,
                UserName = userConfiguration.GetSection("UserName").Value,
                Email= userConfiguration.GetSection("Email").Value,
            };

            await userManager.CreateAsync(user);

            await userManager.AddPasswordAsync(user, userConfiguration.GetSection("Password").Value);
            await userManager.AddToRoleAsync(user, "User");
        }

        private async Task CreateDefaultAdmin(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            var userConfiguration = _configuration.GetSection("AdministrationConfiguration").GetSection("Admin");

            var user = new User
            {
                FirstName = userConfiguration.GetSection("FirstName").Value,
                LastName = userConfiguration.GetSection("LastName").Value,
                UserName = userConfiguration.GetSection("UserName").Value,
                Email = userConfiguration.GetSection("Email").Value,    
            };

            await userManager.CreateAsync(user);

            await userManager.AddPasswordAsync(user, userConfiguration.GetSection("Password").Value);
            await userManager.AddToRoleAsync(user, "Admin");
        }
    }
}
