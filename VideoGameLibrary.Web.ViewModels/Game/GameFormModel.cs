namespace VideoGameLibrary.Web.ViewModels.Game
{
	using System;
	using System.Buffers;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;

	namespace VideoGameLibrary.Web.ViewModels.Game
	{
		using static Common.ValidationConstants.Game;
		using static Common.ValidationConstants.Errors;


		public class GameFormModel
        {
            public GameFormModel()
            {
                Genre = new HashSet<GameGenreSelectFormModel>();
                Platform = new HashSet<GamePlatformSelectFormModel>();
            }

            [Required]
			[StringLength(TitleMaxLength, MinimumLength = TitleMinLength, ErrorMessage = StringLengthErrorMessage)]
            public string Title { get; set; } = null!;

            [Required]
            [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength, ErrorMessage = StringLengthErrorMessage)]
            public string Description { get; set; } = null!;

            [Required]
            [StringLength(DeveloperMaxLength, MinimumLength = DeveloperMinLength)]
            public string Developer { get; set; } = null!;

            [Required]
            [StringLength(PublisherMaxLength, MinimumLength = PublisherMinLength, ErrorMessage = StringLengthErrorMessage)]
            public string Publisher { get; set; } = null!;

            [Required]
            [StringLength(CoverImgMaxLength)]
            [Display(Name = "Image Link")]
            public string CoverImg { get; set; } = null!;

            [Range(PriceMinValue, PriceMaxValue)]
            public decimal Price { get; set; }

            public int GenreId { get; set; }

            public IEnumerable<GameGenreSelectFormModel> Genre { get; set; }

            public int PlatformId { get; set; }

            public IEnumerable<GamePlatformSelectFormModel> Platform { get; set; }

            [Required(ErrorMessage = "The Release Date field is required.")]
            [DataType(DataType.Date)]
            [Display(Name = "Release Date")]
            public DateTime ReleaseDate { get; set; }

            [RegularExpression(NumberOfStarsRegex, ErrorMessage = NumberOfStarsErrorMessage)]
			[Display(Name = "Number of Stars")]
			public string? NumberOfStars { get; set; }
			public string? Rating { get; set; }
            
        }

    }
}
