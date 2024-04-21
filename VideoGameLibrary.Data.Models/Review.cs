using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VideoGameLibrary.Data.Models
{
    using static VideoGameLibrary.Common.ValidationConstants.Review;
    public class Review
    {
        public Review()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        public Guid OwnerId { get; set; }

        [Required]
        [MaxLength(ContentMaxLength)]
        public string Content { get; set; } = null!;

        public int Rating { get; set; }

        public DateTime DatePosted { get; set; }

        [Required]
        [ForeignKey(nameof(Game))]
        public Guid GameId { get; set; }

        public virtual Game Game { get; set; } = null!;
    }
}
