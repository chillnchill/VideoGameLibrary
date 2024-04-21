using System.ComponentModel.DataAnnotations;
using VideoGameLibrary.Web.ViewModels.Game.Enums;

namespace VideoGameLibrary.Web.ViewModels.Game
{
    using static Common.GeneralApplicationConstants;
    public class AllGamesQueryModel
    {
        public AllGamesQueryModel()
        {
            CurrentPage = DefaultPage;
            GamesPerPage = EntitiesPerPage;

            Platforms = new HashSet<string>();
            Genres = new HashSet<string>();
            Games = new HashSet<AllGamesViewModel>();
        }
        public string Platform { get; set; } = null!;
        public string Genre { get; set; } = null!;

        [Display(Name = "Search by Keyword")]
        public string? SearchString { get; set; }

        [Display(Name = "Sort Games")]
        public GameSorting GameSorting { get; set; }

        public int CurrentPage { get; set; }

        [Display(Name = "Show Games On Page")]
        public int GamesPerPage { get; set; }

        public int TotalGames { get; set; }

        public IEnumerable<string> Platforms { get; set; }
        public IEnumerable<string> Genres { get; set; }

        public IEnumerable<AllGamesViewModel> Games { get; set; }
    }
}
