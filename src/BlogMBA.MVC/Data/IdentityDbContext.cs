using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogMBA.MVC.Data
{
    public class IDDbContext : IdentityDbContext
    {
        public IDDbContext(DbContextOptions<IDDbContext> options)
            : base(options)
        {
        }
    }
}
