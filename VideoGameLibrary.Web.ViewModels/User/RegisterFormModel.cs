namespace VideoGameLibrary.Web.ViewModels.User
{
	using System.ComponentModel.DataAnnotations;

	using static VideoGameLibrary.Common.ValidationConstants.User;
	using static VideoGameLibrary.Common.ValidationConstants.Errors;

	public class RegisterFormModel
	{
		[Required]
		[EmailAddress]
		[Display(Name = "Email")]
		public string Email { get; set; } = null!;

		[Required]
		[StringLength(PasswordMaxLength, MinimumLength = PasswordMinLength,
			ErrorMessage = StringLengthErrorMessage)]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; } = null!;

		[DataType(DataType.Password)]
		[Display(Name = "Confirm password")]
		[Compare("Password", ErrorMessage = ConfirmPasswordErrorMessage)]
		public string ConfirmPassword { get; set; } = null!;

		[Required]
		[StringLength(FirstNameMaxLength, MinimumLength = FirstNameMinLength,
			ErrorMessage = StringLengthErrorMessage)]
		public string FirstName { get; set; } = null!;

		[Required]
		[StringLength(LastNameMaxLength, MinimumLength = LastNameMinLength,
			ErrorMessage = StringLengthErrorMessage)]
		public string LastName { get; set; } = null!;

		[Required]
		[StringLength(NicknameMaxLength, MinimumLength = NicknameMinLength,
			ErrorMessage = StringLengthErrorMessage)]
		public string Nickname { get; set; } = null!;
    }
}
