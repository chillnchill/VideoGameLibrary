using System.ComponentModel.DataAnnotations;

namespace VideoGameLibrary.Data.Models
{
    using static VideoGameLibrary.Common.ValidationConstants.Moderator;
    public class Moderator
    {
        public Moderator()
        {
            Id = Guid.NewGuid();
            ManagedGames = new HashSet<GameModerator>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(PhoneNumberMax)]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [MaxLength(AboutMeMaxLength)]
        public string AboutMe { get; set; } = null!;

        public bool IsApproved { get; set; } = false;

        public Guid UserId { get; set; }

        public virtual ApplicationUser User { get; set; } = null!;

        public virtual ICollection<GameModerator> ManagedGames { get; set; }

    }
}
