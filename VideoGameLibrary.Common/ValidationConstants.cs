namespace VideoGameLibrary.Common
{
    public static class ValidationConstants
    {
        public static class Game
        {
            public const int TitleMinLength = 2;
            public const int TitleMaxLength = 350;
            
            public const int DescriptionMinLength = 10;
            public const int DescriptionMaxLength = 2048;

            public const int CoverImgMaxLength = 2048;

            public const int DeveloperMinLength = 2;
            public const int DeveloperMaxLength  = 128;

            public const int PublisherMinLength = 2;
            public const int PublisherMaxLength  = 128;

            public const int RatingMinLength = 1;
            public const int RatingMaxLength  = 32;

            public const int PriceMinValue = 0;
            public const int PriceMaxValue = 500;

            public const int NumberOfStarsMin = 1;
            public const int NumberOfStarsMax = 5;

			public const string DateFormat = "dd/MM/yyyy";

            public const string NumberOfStarsRegex = "^(?:[1-5](?:\\.5)?|5)$";
		}

        public static class Genre
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 35;
        }

        public static class Platform
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 35;
        }

        public static class Review
        {

            public const int ContentMinLength = 10;
            public const int ContentMaxLength = 2048;

            public const int RatingMinValue = 1;
            public const int RatingMaxValue = 5;
        }

        public static class Screenshot
        {
            public const int FileNameMaxLength = 256;
            public const int ContentTypeMaxLength = 128;
        }

        public static class Moderator
        {
            public const int PhoneNumberMin = 7;

            public const int PhoneNumberMax = 15;

            public const int AboutMeMinLength = 32;

            public const int AboutMeMaxLength = 512;

        }

		public static class User
		{
            public const int NicknameMinLength = 2;
            public const int NicknameMaxLength = 22;

			public const int PasswordMinLength = 6;
			public const int PasswordMaxLength = 22;

			public const int FirstNameMinLength = 1;
			public const int FirstNameMaxLength = 15;

			public const int LastNameMinLength = 1;
			public const int LastNameMaxLength = 15;
		}

		public static class Errors
        {
			public const string StringLengthErrorMessage = "The field {0} must be between {2} and {1} characters long";
			public const string NumberOfStarsErrorMessage  = "The field {0} must be between 1 to 5 with or .5 values";
			public const string ConfirmPasswordErrorMessage  = "The password and confirmation password do not match.";
		}
    }
}
