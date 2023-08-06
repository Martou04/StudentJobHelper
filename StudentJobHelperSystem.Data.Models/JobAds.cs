

namespace StudentJobHelperSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static Common.EntityValidationConstants.JobAds;

    public class JobAds
    {
        public JobAds()
        {
            this.Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        public decimal Salary { get; set; }

        [Required]
        [MaxLength(CityOfWorkMaxLength)]
        public string CityOfWork { get; set; } = null!;

        [Required]
        public string TypeOfEmployment { get; set; } = null!;

        public DateTime CreatedOn { get; set; }

        public bool IsActive { get; set; }

        public int OffDaysCount { get; set; }

        [Required]
        [MaxLength(ForeignLanguageMaxLength)]
        public string ForeignLanguage { get; set; } = null!;

        public bool HomeOffice { get; set; }

        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [MaxLength(EmailMaxLength)]
        public string Email { get; set; } = null!;

        [Required]
        [MaxLength(LogoUrlMaxLength)]
        public string LogoUrl { get; set; } = null!;

        public int CategoryId { get;set; }

        public virtual Category Category { get; set; } = null!;

        public Guid EmployerId { get; set; }
        public virtual Employer Employer { get; set; } = null!;

        public Guid? WorkerId { get; set; }
        public virtual ApplicationUser? Worker { get; set; }
    }
}
