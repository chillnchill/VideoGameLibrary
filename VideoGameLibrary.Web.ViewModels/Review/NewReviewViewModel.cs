using System.ComponentModel.DataAnnotations;

namespace VideoGameLibrary.Web.ViewModels.Review
{
	using static Common.ValidationConstants.Errors;
	using static Common.ValidationConstants.Review;
	public class NewReviewViewModel
	{
		[Required]
		public string Id { get; set; } = null!;

        [Required]
		public string OwnerId { get; set; } = null!;

		[Required]
		[MaxLength(ContentMaxLength)]
		[StringLength(ContentMaxLength, MinimumLength = ContentMinLength, ErrorMessage = StringLengthErrorMessage)]
		public string Content { get; set; } = null!;

		[Display(Name = "Number of Stars (from 1 to 5)")]
		[Range(RatingMinValue, RatingMaxValue)]
		public int Rating { get; set; }

		[Required]
		public string GameId { get; set; } = null!;

	}
}
