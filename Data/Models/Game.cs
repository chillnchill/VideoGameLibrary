using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VideoGameLibrary.Common;

namespace VideoGameLibrary.Data.Models
{
    public class Game
    {
        public Game()
        {
            Screenshots = new HashSet<Screenshot>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.GameTitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        [Required]
        public string CoverImg { get; set; } = null!;

        [Required]
        public string Developer { get; set; } = null!;

        [Required]
        public string Publisher { get; set; } = null!;

        [Required]
        //ESRB/PEGI/etc
        public string Rating { get; set; } = null!;

        //by player reviews, made it nullable because there might be no reviews
        public double? PlayerRating { get; set; }

        public DateTime ReleaseDate { get; set; }

        public decimal Price { get; set; }

        public int? GameLength { get; set; }

        public int NumberOfPlayers { get; set; }

        [ForeignKey(nameof(Genre))]
        public int GenreId { get; set; }

        public virtual Genre Genre { get; set; } = null!;

        [ForeignKey(nameof(Platform))]
        public int PlatformId { get; set; }

        public virtual Platform Platforms { get; set; } = null!;

        [ForeignKey(nameof(Review))]
        public int ReviewId { get; set; }

        public virtual Review Reviews { get; set; } = null!;

        public virtual ICollection<Screenshot> Screenshots { get; set; }

    }
}
