using BlogMBA.Business.Models.Base;
using BlogMBA.Business.Models.Comentarios;

namespace BlogMBA.Business.Models.Comentarios
{
    public interface IComentarioRepository : IRepository<Comentario>
    {
        Task<IEnumerable<Comentario>> ObterComentarios();
        Task<Comentario> ObterComentarioPorId(Guid id);
    }
}