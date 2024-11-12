using BlogMBA.Business.Models.Autores;
using BlogMBA.Business.Models.Base;
using BlogMBA.Business.Models.Comentarios;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogMBA.Business.Models.Posts
{
    public class Post : Entity
    {
        public Guid AutorId { get; set; }

        [ForeignKey(nameof(AutorId))]
        public Autor Autor { get; set; }
        
        [StringLength(100)]
        [Required(ErrorMessage ="O campo {0} é obrigatório.")]
        public string Titulo { get; set; }

        [StringLength(800)]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Texto { get; set; }

        public DateTime DataCriacao { get; set; } = DateTime.Now;

        public IEnumerable<Comentario> Comentarios { get; set; }
    }
}
