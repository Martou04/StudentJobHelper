

namespace StudentJobHelperSystem.Web.ViewModels.JobAd
{
    using StudentJobHelperSystem.Web.ViewModels.Category;

    using System.ComponentModel.DataAnnotations;

    using static Common.EntityValidationConstants.JobAds;
    public class JobAdFormModel
    {
        public JobAdFormModel()
        {
            this.Categories = new HashSet<JobAdSelectCategoryFormModel>();
        }

        [Required]
        [StringLength(TitleMaxLength, MinimumLength =TitleMinLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLength,MinimumLength =DescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Range(typeof(decimal),SalaryMinValue,SalaryMaxValue)]
        [Display(Name ="Monthly Salary")]
        public decimal Salary { get; set; }

        [Required]
        [StringLength(CityOfWorkMaxLength,MinimumLength =CityOfWorkMinLength)]
        [Display(Name ="City of workplace")]
        public string CityOfWork { get; set; } = null!;

        [Required]
        [Display(Name ="Employment type")]
        public string TypeOfEmployment { get; set; } = null!;

        [Required]
        [StringLength(ForeignLanguageMaxLength,MinimumLength =ForeignLanguageMinLength)]
        [Display(Name ="Foreign language")]
        public string ForeignLanguage { get; set; } = null!;

        [Display(Name ="Number of days off")]
        public int OffDaysCount { get; set; }

        [Display(Name ="Opportunities to work from home")]
        public bool HomeOffice { get; set; }

        [Required]
        [StringLength(PhoneNumberMaxLength,MinimumLength =PhoneNumberMinLength)]
        [Display(Name ="Phone number")]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [StringLength(EmailMaxLength,MinimumLength =EmailMinLength)]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(LogoUrlMaxLength)]
        [Display(Name ="Logo image link")]
        public string LogoUrl { get; set; } = null!;

        [Display(Name ="Category")]
        public int CategoryId { get; set; }

        public IEnumerable<JobAdSelectCategoryFormModel> Categories { get; set; }
    }
}
