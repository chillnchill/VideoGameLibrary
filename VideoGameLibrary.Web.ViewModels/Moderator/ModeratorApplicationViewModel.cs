using System.ComponentModel.DataAnnotations;

namespace VideoGameLibrary.Web.ViewModels.Moderator
{
    using static VideoGameLibrary.Common.ValidationConstants.Moderator;
    using static Common.ValidationConstants.Errors;
    public class ModeratorApplicationViewModel
    {
        public string? Id { get; set; }
        public string? Nickname { get; set; }

        [Required]
        [Phone]
        [StringLength(PhoneNumberMax, MinimumLength = PhoneNumberMin, ErrorMessage = StringLengthErrorMessage)]
        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [StringLength(AboutMeMaxLength, MinimumLength = AboutMeMinLength, ErrorMessage = StringLengthErrorMessage)]
        public string AboutMe { get; set; } = null!;

        public bool IsApproved { get; set; } = false;

        public bool IsSubmitted { get; set; } = false;
    }
}
