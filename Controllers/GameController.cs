using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VideoGameLibrary.Services.Data.Interfaces;

namespace VideoGameLibrary.Controllers
{
    [Authorize]
    public class GameController : Controller
    {
        private readonly IGameService gameService;
        public GameController(IGameService gameService)
        {
            this.gameService = gameService;
        }

        [AllowAnonymous] 
        public async Task<IActionResult> AllGames()
        {
            var allGames = await gameService.GetAllGamesAsync();
            return View(allGames);
        }
    }
}
