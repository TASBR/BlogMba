using BlogMBA.Business.Models.Autores;
using BlogMBA.Business.Models.Base;
using BlogMBA.Business.Models.Posts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogMBA.Business.Models.Comentarios
{
    public class Comentario : Entity
    {
        public Guid PostId { get; set; }

        [ForeignKey(nameof(PostId))]
        public Post Post { get; set; }

        public Guid? AutorId { get; set; }

        [ForeignKey(nameof(AutorId))]
        public Autor Autor { get; set; }

        [StringLength(250)]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Texto { get; set; }

        public DateTime DataInsercao { get; set; } = DateTime.Now;
    }
}
