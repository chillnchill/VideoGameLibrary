using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VideoGameLibrary.Data.Models
{
	using static VideoGameLibrary.Common.ValidationConstants.Game;
	public class Game
	{
		public Game()
		{
			Id = Guid.NewGuid();
			Reviews = new HashSet<Review>();
			Moderators = new HashSet<GameModerator>();
		}

		[Key]
		public Guid Id { get; set; }

		[Required]
		[MaxLength(TitleMaxLength)]
		public string Title { get; set; } = null!;

		[Required]
		[MaxLength(DescriptionMaxLength)]
		public string Description { get; set; } = null!;

		[Required]
		[MaxLength(CoverImgMaxLength)]
		public string CoverImg { get; set; } = null!;

		[Required]
		[MaxLength(DeveloperMaxLength)]
		public string Developer { get; set; } = null!;

		[Required]
		[MaxLength(PublisherMaxLength)]
		public string Publisher { get; set; } = null!;

		[MaxLength(RatingMaxLength)]
		//ESRB/PEGI/etc
		public string? Rating { get; set; }

		public bool IsDeleted { get; set; } = false;
		//changed player reviews to
		//(this will be given via users, from 1 to 5

		[MaxLength(NumberOfStarsMax)]
		public double? NumberOfStars { get; set; }

		public DateTime ReleaseDate { get; set; }

		public decimal Price { get; set; }

		[ForeignKey(nameof(Genre))]
		public int GenreId { get; set; }

		public virtual Genre Genre { get; set; } = null!;

		[ForeignKey(nameof(Platform))]
		public int PlatformId { get; set; }

		public virtual Platform Platform { get; set; } = null!;

		public virtual ApplicationUser? Owner { get; set; }

		public Guid OwnerId { get; set; }

		public virtual ICollection<Review> Reviews { get; set; }

		public virtual ICollection<GameModerator> Moderators { get; set; }
	}
}
