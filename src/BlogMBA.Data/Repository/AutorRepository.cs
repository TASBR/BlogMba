using BlogMBA.Business.Models.Autores;
using BlogMBA.Business.Models.Posts;
using BlogMBA.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace BlogMBA.Data.Repository
{
    public class AutorRepository : Repository<Autor>, IAutorRepository
    {
        public AutorRepository(AppDbContext context) : base(context) { }
        
        /// <param name="id">Repasse o id do usuário para chegar até o autor.</param>
        /// <returns></returns>
        public async Task<Autor> ObterAutorPorIdUsuario(Guid id)
        {
            return await Db.Autores.AsNoTracking().FirstOrDefaultAsync(p => p.UserId == id) ?? new Autor();
        }
    }
}