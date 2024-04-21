using VideoGameLibrary.Web.ViewModels.Game;

namespace VideoGameLibrary.Services.Data.Models.Game
{
	public class AllGamesFilteredAndSortingModel
	{
        public AllGamesFilteredAndSortingModel()
        {
            Games = new HashSet<AllGamesViewModel>();
        }
        public int TotalGamesCount { get; set; }

		public IEnumerable<AllGamesViewModel> Games { get; set; }
	}
}
