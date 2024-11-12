using BlogMBA.Business.Models.Comentarios;
using BlogMBA.Business.Models.Posts;
using BlogMBA.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace BlogMBA.Data.Repository
{
    public class ComentarioRepository : Repository<Comentario>, IComentarioRepository
    {
        public ComentarioRepository(AppDbContext context) : base(context) { }

        public async Task<Comentario> ObterComentarioPorId(Guid id)
        {
            return  await Db.Comentarios.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id) ?? new Comentario();
        }

        public async Task<IEnumerable<Comentario>> ObterComentarios()
        {
            return await Db.Comentarios.AsNoTracking().OrderBy(p => p.DataInsercao).ToListAsync();
        }
    }
}