using System.ComponentModel.DataAnnotations;
using VideoGameLibrary.Web.ViewModels.Game;

namespace VideoGameLibrary.Web.ViewModels.Genre
{
	using static Common.ValidationConstants.Errors;
	using static Common.ValidationConstants.Genre;
	public class NewGenreViewModel
	{
		public NewGenreViewModel()
		{
			ExistingGenres = new HashSet<GameGenreSelectFormModel>();
		}

		public int Id { get; set; }

		[StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = StringLengthErrorMessage)]
		[Display(Name = "New Genre Name")]
		public string NewGenreName { get; set; } = null!;

		public IEnumerable<GameGenreSelectFormModel> ExistingGenres { get; set; }
	}
}
