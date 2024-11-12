using BlogMBA.Business.Models.Autores;
using BlogMBA.Business.Models.Posts;
using BlogMBA.MVC.Configurations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogMBA.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/posts")]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly IAutorRepository _autorRepository;
        private readonly ICustomUserManager _customUserManager;

        public PostsController(IPostRepository postRepository,
                       IAutorRepository autorRepository,
                       ICustomUserManager customUserManager)
        {
            _postRepository = postRepository;
            _autorRepository = autorRepository;
            _customUserManager = customUserManager;
        }

        [HttpGet("todos")]        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<IEnumerable<Post>>> GetPost()
        {
            if (_postRepository.ObterPosts() == null)
            {
                return NotFound();
            }

            return Ok(await _postRepository.ObterPosts());
        }
        
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Post>> GetPostById(string id)
        {
            if (_postRepository.ObterPosts == null)
            {
                return NotFound();
            }

            var post = await _postRepository.ObterPostPorId(Guid.Parse(id));

            if (post == null)
            {
                return NotFound();
            }

            return post;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Post>> PostProduto(Post post)
        {
            if (_postRepository.ObterPosts == null)
            {
                return Problem("Erro ao criar um produto, contate o suporte!");
            }

            if (!ModelState.IsValid)
            {
                return ValidationProblem(new ValidationProblemDetails(ModelState)
                {
                    Title = "Um ou mais erros de validação ocorreram!"
                });
            }

            await _postRepository.Adicionar(post);            

            return CreatedAtAction(nameof(GetPostById), new { id = post.Id });
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> PutProduto(string id, Post post)
        {
            if (id != post.Id.ToString()) return BadRequest();

            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            try
            {
                await _postRepository.Atualizar(post);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteProduto(string id)
        {
            if (_postRepository.ObterPosts == null)
            {
                return NotFound();
            }

            var post = await _postRepository.ObterPostPorId(Guid.Parse(id));

            if (post == null)
            {
                return NotFound();
            }

            await _postRepository.Remover(Guid.Parse(id));

            return NoContent();
        }


        private bool PostExists(string id)
        {
            return _postRepository.ObterPostPorId(Guid.Parse(id)) != null ? true : false;
        }
    }
}
