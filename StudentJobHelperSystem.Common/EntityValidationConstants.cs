namespace StudentJobHelperSystem.Common
{
    public static class EntityValidationConstants
    {
        public static class Category
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 50;
        }

        public static class JobAds
        {
            public const int TitleMinLength = 3;
            public const int TitleMaxLength = 100;

            public const int DescriptionMinLength = 50;
            public const int DescriptionMaxLength = 5000;

            public const int LogoUrlMaxLength = 2048;

            public const int CityOfWorkMinLength = 3;
            public const int CityOfWorkMaxLength = 30;

            public const int TypeOfEmploymentMinLength = 3;
            public const int TypeOfEmploymentMaxLength = 50;

            public const int ForeignLanguageMinLength = 3;
            public const int ForeignLanguageMaxLength = 30;

            public const int PhoneNumberMinLength = 8;
            public const int PhoneNumberMaxLength = 15;

            public const int EmailMinLength = 7;
            public const int EmailMaxLength = 35;
        }

        public static class Employer
        {
            public const int CompanyNumberMinLength = 9; 
            public const int CompanyNumberMaxLength = 15;
        }


    }
}
