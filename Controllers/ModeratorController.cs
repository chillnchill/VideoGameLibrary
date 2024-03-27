using Microsoft.AspNetCore.Mvc;
using VideoGameLibrary.Services.Data.Interfaces;
using VideoGameLibrary.Web.Infrastructure.Extensions;

namespace VideoGameLibrary.Controllers
{
    using static VideoGameLibrary.Common.NotificationMessagesConstants;
    public class ModeratorController : Controller
    {
        private readonly IModeratorService moderatorService;

        public ModeratorController(IModeratorService moderatorService)
        {
            this.moderatorService = moderatorService;
        }

        [HttpGet]
        public async Task<IActionResult> Become()
        {
            string? userId = User.GetId();
            bool isAgent = await moderatorService.ModeratorExistsByUserIdAsync(userId!);
            if (isAgent)
            {
                TempData[ErrorMessage] = "You're a moderator already!";

                return RedirectToAction("All", "Game");
            }

            return View();
        }

    }
}
