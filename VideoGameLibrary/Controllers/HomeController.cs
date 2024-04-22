using Microsoft.AspNetCore.Mvc;
using static VideoGameLibrary.Common.GeneralApplicationConstants;

namespace VideoGameLibrary.Controllers
{

	public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (User.IsInRole(AdminRoleName))
            {
                return this.RedirectToAction("Index", "Home", new { Area = AdminAreaName });
            }
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode)
        {
            if (statusCode == 400 || statusCode == 404)
            {
                return View("Error404");
            }

            if (statusCode == 401)
            {
                return View("Error401");
            }

            return View();
        }
    }
}
