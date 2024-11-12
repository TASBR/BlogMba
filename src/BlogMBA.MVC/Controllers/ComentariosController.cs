using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BlogMBA.Business.Models.Comentarios;
using BlogMBA.Data.Context;
using BlogMBA.Business.Models.Posts;
using BlogMBA.Business.Models.Autores;
using BlogMBA.MVC.Configurations;

namespace BlogMBA.MVC.Controllers
{
    public class ComentariosController : Controller
    {
        private readonly IComentarioRepository _comentarioRepository;
        private readonly IPostRepository _postRepository;
        private readonly IAutorRepository _autorRepository;
        private readonly ICustomUserManager _customUserManager;

        public ComentariosController(IComentarioRepository comentarioRepository,
                                     IPostRepository postRepository,
                                     IAutorRepository autorRepository,
                                     ICustomUserManager customUserManager)
        {
            _comentarioRepository = comentarioRepository;
            _postRepository = postRepository;
            _autorRepository = autorRepository;
            _customUserManager = customUserManager;
        }

        [NonAction]
        public async Task<IActionResult> Index()
        {            
            return View(await _comentarioRepository.ObterComentarios());
        }
        
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comentario = await _comentarioRepository.ObterComentarioPorId(id);
            if (comentario == null)
            {
                return NotFound();
            }

            return View(comentario);
        }
        
        public IActionResult Create(string postId)
        {
            ViewBag.postId = postId;
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Texto")] Comentario comentario, string postId)
        {
            if (ModelState.IsValid)
            {
                var userId = _customUserManager.GetIdentityUserByName(User.Identity.Name).Result.Id;
                var autorId = _autorRepository.ObterAutorPorIdUsuario(Guid.Parse(userId)).Result.Id;
                comentario.Id = Guid.NewGuid();
                comentario.PostId = Guid.Parse(postId);
                comentario.AutorId = autorId;
                await _comentarioRepository.Adicionar(comentario);

                return RedirectToAction("Details","Posts", new { id = postId});
            }
            return View(comentario);
        }

        
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comentario = await _comentarioRepository.ObterComentarioPorId(id);
            if (comentario == null)
            {
                return NotFound();
            }

            return View(comentario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Texto")] Comentario comentario, string postId)
        {
            if (id != comentario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    comentario.PostId = Guid.Parse(postId);
                    await _comentarioRepository.Atualizar(comentario);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ComentarioExists(comentario.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Posts", new { id = postId });
            }
            return View(comentario);
        }

        // GET: Comentarios/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comentario = await _comentarioRepository.ObterComentarioPorId(id);
            if (comentario == null)
            {
                return NotFound();
            }

            return View(comentario);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id, string postId)
        {
            var comentario = await _comentarioRepository.ObterComentarioPorId(id);
            if (comentario != null)
            {
                await _comentarioRepository.Remover(id);
            }
            return RedirectToAction("Details", "Posts", new { id = postId });

        }

        private async Task<bool> ComentarioExists(Guid id)
        {
            var result = await _comentarioRepository.ObterComentarioPorId(id);
            if (result != null)
                return true;
            return false;
        }
    }
}
