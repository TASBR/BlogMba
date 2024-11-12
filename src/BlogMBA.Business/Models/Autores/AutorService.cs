using BlogMBA.Business.Models.Autores;
using BlogMBA.Business.Models.Base;
using BlogMBA.Business.Notificacoes;

namespace BlogMBA.Business.Models.Autors
{
    public class AutorService : BaseService, IAutorService
    {
        private readonly IAutorRepository _autorRepository;

        public AutorService(IAutorRepository autorRepository,
                              INotificador notificador) : base(notificador)
        {
            _autorRepository = autorRepository;
        }

        public async Task Adicionar(Autor autor)
        {
            await _autorRepository.Adicionar(autor);
        }

        public async Task Atualizar(Autor autor)
        {
            await _autorRepository.Atualizar(autor);
        }

        public async Task Remover(Guid id)
        {
            await _autorRepository.Remover(id);
        }

        public void Dispose()
        {
            _autorRepository?.Dispose();
        }
    }
}