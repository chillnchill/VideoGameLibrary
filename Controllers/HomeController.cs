using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

using VideoGameLibrary.Models;
using VideoGameLibrary.Services.Data.Interfaces;


namespace VideoGameLibrary.Controllers
{

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode)
        {
            if (statusCode == 404 || statusCode == 400)
            {
                return View("Error404");
            }

            return View();
        }
    }
}
