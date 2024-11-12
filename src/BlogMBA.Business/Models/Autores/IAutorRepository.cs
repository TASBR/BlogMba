using BlogMBA.Business.Models.Base;

namespace BlogMBA.Business.Models.Autores
{
    public interface IAutorRepository : IRepository<Autor>
    {        
        Task<Autor> ObterAutorPorIdUsuario(Guid id);
    }
}