using Microsoft.AspNetCore.Mvc;

namespace VideoGameLibrary.Areas.Admin.Controllers
{
    public class HomeController : BaseAdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
