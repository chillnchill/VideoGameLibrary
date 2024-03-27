using Microsoft.AspNetCore.Mvc;

namespace VideoGameLibrary.Controllers
{
    public class GenreController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
