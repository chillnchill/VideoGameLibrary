using System.ComponentModel.DataAnnotations;
using VideoGameLibrary.Web.ViewModels.Game;

namespace VideoGameLibrary.Web.ViewModels.Platform
{
	using static Common.ValidationConstants.Errors;
	using static Common.ValidationConstants.Platform;
	public class NewPlatformViewModel
	{
		public NewPlatformViewModel()
		{
			ExistingPlatforms = new HashSet<GamePlatformSelectFormModel>();
		}

		public int Id { get; set; }

		[StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = StringLengthErrorMessage)]
		[Display(Name = "New Platform Name")]
		public string NewPlatformName { get; set; } = null!;

		public IEnumerable<GamePlatformSelectFormModel> ExistingPlatforms { get; set; }
	}
}
