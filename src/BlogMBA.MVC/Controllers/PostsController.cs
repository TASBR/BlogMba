using BlogMBA.Business.Models.Autores;
using BlogMBA.Business.Models.Posts;
using BlogMBA.MVC.Configurations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogMBA.MVC.Controllers
{
    [Authorize]
    public class PostsController : Controller
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

        [NonAction]
        public async Task<IActionResult> Index()
        {           
            return View(await _postRepository.ObterPosts());
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(string id)
        {
            var post = await _postRepository.ObterPostPorId(Guid.Parse(id));
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }
        
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Titulo,Texto")] Post post)
        {
            if (ModelState.IsValid)
            {
                var userId = _customUserManager.GetIdentityUserByName(User.Identity.Name).Result.Id;
                var autorId = _autorRepository.ObterAutorPorIdUsuario(Guid.Parse(userId)).Result.Id;
                post.Id = Guid.NewGuid();
                post.AutorId = autorId;
                await _postRepository.Adicionar(post);
                return RedirectToAction("Index", "Home");
            }
            return View(post);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _postRepository.ObterPostPorId(id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Titulo,Texto")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _postRepository.Atualizar(post);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!( await PostExists(post.Id)))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction("Index", "Home");
        }
        
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _postRepository.ObterPostPorId(id);
               
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var post = await _postRepository.ObterPostPorId(id);
            if (post != null)
            {
                await _postRepository.Remover(id);
            }

            return RedirectToAction("Index", "Home");
        }

        private async Task<bool> PostExists(Guid id)
        {
            var result = await _postRepository.ObterPostPorId(id);
            if(result != null)
                return true;
            return false;
        }
    }
}
