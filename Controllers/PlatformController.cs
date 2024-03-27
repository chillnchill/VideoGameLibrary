using Microsoft.AspNetCore.Mvc;

namespace VideoGameLibrary.Controllers
{
    public class PlatformController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
