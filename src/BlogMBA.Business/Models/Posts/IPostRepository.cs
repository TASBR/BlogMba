using BlogMBA.Business.Models.Base;

namespace BlogMBA.Business.Models.Posts
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<IEnumerable<Post>> ObterPosts();
        Task<Post> ObterPostPorId(Guid id);
    }
}