using System.ComponentModel.DataAnnotations;

namespace VideoGameLibrary.Web.ViewModels.Game
{
    public class AllGamesViewModel
    {
        public string Id { get; set; } = null!;
        public string Title { get; set; } = null!;

		[Display(Name = "Cover Image")]
		public string CoverImg { get; set; } = null!;
        public string Developer { get; set; } = null!;
        public string Publisher { get; set; } = null!; 
        public string Genre { get; set; } = null!;

        public decimal Price { get; set; }

        [Display(Name = "Release Date")]
        public string ReleaseDate { get; set; } = null!;

	}
}
