namespace VideoGameLibrary.Web.ViewModels.Review
{
	public class AllReviewsViewModel
	{
		public string Id { get; set; } = null!;
		public string Author { get; set; } = null!;

		public string Content { get; set; } = null!;

        public int Rating { get; set; }

        public string Date { get; set; } = null!;
    }
}
