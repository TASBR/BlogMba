using BlogMBA.Data.Context;
using BlogMBA.MVC.Data;
using Microsoft.EntityFrameworkCore;

namespace BlogMBA.MVC.Configurations
{
    public static class DatabaseSelectorExtension
    {
        public static void DatabaseSelector(this WebApplicationBuilder builder)
        {
            if (builder.Environment.IsDevelopment())
            {
                builder.Services.AddDbContext<IDDbContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnectionSQLite")));

                builder.Services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnectionSQLite")));
            }
            else
            {
                builder.Services.AddDbContext<IDDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

                builder.Services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            }

        }
    }
}
