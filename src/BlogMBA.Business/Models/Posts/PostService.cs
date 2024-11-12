using BlogMBA.Business.Models.Base;
using BlogMBA.Business.Models.Produtos;
using BlogMBA.Business.Notificacoes;

namespace BlogMBA.Business.Models.Posts
{
    public class PostService : BaseService, IPostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository,
                              INotificador notificador) : base(notificador)
        {
            _postRepository = postRepository;
        }

        public async Task Adicionar(Post post)
        {
            if (!ExecutarValidacao(new PostValidation(), post)) return;

            await _postRepository.Adicionar(post);
        }

        public async Task Atualizar(Post post)
        {
            if (!ExecutarValidacao(new PostValidation(), post)) return;

            await _postRepository.Atualizar(post);
        }

        public async Task Remover(Guid id)
        {
            await _postRepository.Remover(id);
        }

        public void Dispose()
        {
            _postRepository?.Dispose();
        }
    }
}