using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace VideoGameLibrary.Data.Models
{
	using static VideoGameLibrary.Common.ValidationConstants.User;
	using static VideoGameLibrary.Common.ValidationConstants.Moderator;
	public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
			Id = Guid.NewGuid();

			AddedGames = new HashSet<Game>();
			LikedGames = new HashSet<Game>();
		}
		
		[Required]
        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(LastNameMaxLength)]
        public string LastName { get; set; } = null!;

        [Required]
        [MaxLength(NicknameMaxLength)]
        public string Nickname { get; set; } = null!;


		[MaxLength(PhoneNumberMax)]
		public string? Phone { get; set; }

		[MaxLength(AboutMeMaxLength)]
		public string? AboutMe { get; set; }

		public bool IsSubmitted { get; set; } = false;
		public virtual ICollection<Game> AddedGames { get; set; } = null!;
        public virtual ICollection<Game> LikedGames { get; set; } = null!;
    }
}
