using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VideoGameLibrary.Data.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Game))]
        public int GameId { get; set; }

        public virtual Game Game { get; set; } = null!;

        public string Author { get; set; } = null!;

        public string Content { get; set; } = null!;

        public int Rating { get; set; }

        public DateTime DatePosted { get; set; }
    }
}
