using System.ComponentModel.DataAnnotations.Schema;

namespace VideoGameLibrary.Data.Models
{
    public class UserModerator
    {
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public virtual ApplicationUser User { get; set; } = null!;

        [ForeignKey(nameof(Moderator))]
        public Guid ModeratorId { get; set; }
        public virtual Moderator Moderator { get; set; } = null!;
    }
}
