using BlogMBA.Business.Models.Posts;
using BlogMBA.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace BlogMBA.Data.Repository
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(AppDbContext context) : base(context) { }

        public async Task<Post> ObterPostPorId(Guid id)
        {
            return await Db.Posts.AsNoTracking()
                                 .Include(p=>p.Autor)
                                 .Include(p => p.Comentarios)
                                 //.ThenInclude(p=> p.au)
                                 .FirstOrDefaultAsync(p => p.Id == id) ?? new Post();
        }

        public async Task<IEnumerable<Post>> ObterPosts()
        {
            return await Db.Posts.AsNoTracking()
                                 .OrderBy(p => p.Titulo)
                                 .ToListAsync();
        }
    }
}