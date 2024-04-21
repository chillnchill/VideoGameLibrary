using System.ComponentModel.DataAnnotations.Schema;

namespace VideoGameLibrary.Data.Models
{
    public class GameModerator
    {
        [ForeignKey(nameof(Moderator))]
        public Guid ModeratorId { get; set; }
        public virtual Moderator Moderator { get; set; } = null!;

        [ForeignKey(nameof(Game))]
        public Guid GameId { get; set; }
        public virtual Game Game { get; set; } = null!;
    }
}
