using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VideoGameLibrary.Data.Models
{
    public class GamePlatform
    {
        [Required]
        [ForeignKey(nameof(Game))]
        public int GameId { get; set; }

        public Game Game { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Platform))]
        public int PlatformId { get; set; }

        public virtual Platform Platform { get; set; } = null!;
    }
}
