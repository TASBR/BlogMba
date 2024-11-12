using BlogMBA.Business.Models.Comentarios;

namespace BlogMBA.Business.Models.Comentarios
{
    public interface IComentarioService : IDisposable
    {
        Task Adicionar(Comentario comentario);
        Task Atualizar(Comentario comentario);
        Task Remover(Guid id);
    }
}