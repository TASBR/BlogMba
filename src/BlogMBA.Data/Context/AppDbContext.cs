using BlogMBA.Business.Models.Autores;
using BlogMBA.Business.Models.Comentarios;
using BlogMBA.Business.Models.Posts;
using Microsoft.EntityFrameworkCore;

namespace BlogMBA.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Autor> Autores { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Comentario> Comentarios { get; set; }
    }
}
