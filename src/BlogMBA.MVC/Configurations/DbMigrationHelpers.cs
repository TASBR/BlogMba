using BlogMBA.Data.Context;
using BlogMBA.MVC.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace BlogMBA.MVC.Configurations
{
    public static class DbMigrationHelperExtension
    {
        public static void UseDbMigrationHelper(this WebApplication app)
        {
            DbMigrationHelpers.EnsureSeedData(app).Wait();
        }
    }

    public static class DbMigrationHelpers
    {
        public static async Task EnsureSeedData(WebApplication serviceScope)
        {
            var services = serviceScope.Services.CreateScope().ServiceProvider;
            await EnsureSeedData(services);
        }

        public static async Task EnsureSeedData(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var contextId = scope.ServiceProvider.GetRequiredService<IDDbContext>();

            if (env.IsDevelopment() || env.IsEnvironment("Docker") || env.IsStaging())
            {
                await context.Database.MigrateAsync();
                if (contextId.Database.GetPendingMigrationsAsync().Result.Count() > 0)
                    await contextId.Database.MigrateAsync();

                await EnsureSeedProducts(context, contextId);
            }
        }

        private static async Task EnsureSeedProducts(AppDbContext context, IDDbContext contextId)
        {
            if (contextId.Users.Any())
                return;
            var idUser = Guid.NewGuid().ToString();
            await contextId.Users.AddAsync(new IdentityUser
            {
                Id = idUser,
                UserName = "admin@blogmba.com",
                NormalizedUserName = "ADMIN@BLOGMBA.COM",
                Email = "admin@blogmba.com",
                NormalizedEmail = "ADMIN@BLOGMBA.COM",
                AccessFailedCount = 0,
                LockoutEnabled = false,
                PasswordHash = "AQAAAAIAAYagAAAAEEdWhqiCwW/jZz0hEM7aNjok7IxniahnxKxxO5zsx2TvWs4ht1FUDnYofR8JKsA5UA==",//Teste@123
                TwoFactorEnabled = false,
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            });

            if (contextId.Roles.Any(u => u.Name == "Admin"))
                return;

            var idRoleAdmin = Guid.NewGuid().ToString();
            await contextId.Roles.AddAsync(new IdentityRole
            {
                Id = idRoleAdmin,
                Name = "Admin",
                ConcurrencyStamp = idUser,
                NormalizedName = "ADMIN"
            });

            await contextId.UserRoles.AddAsync(new IdentityUserRole<string>
            {
                UserId = idUser,
                RoleId = idRoleAdmin
            });

            await contextId.SaveChangesAsync();
        }
    }
}
