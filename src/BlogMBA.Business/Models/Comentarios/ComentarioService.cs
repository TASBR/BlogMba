using BlogMBA.Business.Models.Base;
using BlogMBA.Business.Notificacoes;

namespace BlogMBA.Business.Models.Comentarios
{
    public class ComentarioService : BaseService, IComentarioService
    {
        private readonly IComentarioRepository _comentarioRepository;

        public ComentarioService(IComentarioRepository comentarioRepository,
                              INotificador notificador) : base(notificador)
        {
            _comentarioRepository = comentarioRepository;
        }

        public async Task Adicionar(Comentario comentario)
        {
            if (!ExecutarValidacao(new ComentarioValidation(), comentario)) return;

            await _comentarioRepository.Adicionar(comentario);
        }

        public async Task Atualizar(Comentario comentario)
        {
            if (!ExecutarValidacao(new ComentarioValidation(), comentario)) return;

            await _comentarioRepository.Atualizar(comentario);
        }

        public async Task Remover(Guid id)
        {
            await _comentarioRepository.Remover(id);
        }

        public void Dispose()
        {
            _comentarioRepository?.Dispose();
        }
    }
}