using BlogMBA.Business.Models.Posts;
using Microsoft.AspNetCore.Mvc;

namespace BlogMBA.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostRepository _postRepository;

        public HomeController(ILogger<HomeController> logger, IPostRepository postRepository)
        {
            _logger = logger;
            _postRepository = postRepository;
        }

        public async Task<IActionResult> Index()
        {
            var posts = await _postRepository.ObterPosts();
            return View(posts);
        }
    }
}
