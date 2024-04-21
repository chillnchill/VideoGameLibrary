using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VideoGameLibrary.Data.Models
{
    using static VideoGameLibrary.Common.ValidationConstants.Screenshot;
    public class Screenshot
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(FileNameMaxLength)]
        public string FileName { get; set; } = null!;

        [Required]
        [MaxLength(ContentTypeMaxLength)]
        public string ContentType { get; set; } = null!; 

        //for file size in bytes
        public int Size { get; set; }  

        [ForeignKey(nameof(Game))]
        public Guid GameId { get; set; }

        public virtual Game Game { get; set; } = null!;
    }
}
