using System.ComponentModel.DataAnnotations;

namespace VideoGameLibrary.Web.ViewModels.User
{
    using static VideoGameLibrary.Common.ValidationConstants.Errors;
    using static VideoGameLibrary.Common.ValidationConstants.User;
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; } = null!;

        [Required]
        [StringLength(PasswordMaxLength, MinimumLength = PasswordMinLength,
            ErrorMessage = ConfirmPasswordErrorMessage)]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string Password { get; set; } = null!;

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = ConfirmPasswordErrorMessage)]
        public string ConfirmPassword { get; set; } = null!;
    }
}
