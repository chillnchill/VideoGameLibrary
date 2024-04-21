using VideoGameLibrary.Web.ViewModels.Review;

namespace VideoGameLibrary.Web.ViewModels.Game
{
    public class GameDetailsViewModel
    {
        public string Id { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string CoverImg { get; set; } = null!;
        public string ReleaseDate { get; set; } = null!;
        public decimal Price { get; set; }
        public double? NumberOfStars { get; set; }
        public string Genre { get; set; } = null!;
        public string Platform { get; set; } = null!;
        public string? Rating { get; set; }
        public bool IsOwner { get; set; }

		public IEnumerable<AllReviewsViewModel> Reviews { get; set; }
	}
}
