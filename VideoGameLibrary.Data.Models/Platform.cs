using System.ComponentModel.DataAnnotations;

namespace VideoGameLibrary.Data.Models
{
    using static VideoGameLibrary.Common.ValidationConstants.Platform;
    public class Platform
    {
        public Platform()
        {
            Games = new HashSet<Game>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;
        public virtual ICollection<Game> Games { get; set; }
    }
}