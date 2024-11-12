using BlogMBA.Business.Models.Posts;

namespace BlogMBA.Business.Models.Posts
{
    public interface IPostService : IDisposable
    {
        Task Adicionar(Post post);
        Task Atualizar(Post post);
        Task Remover(Guid id);
    }
}