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
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
