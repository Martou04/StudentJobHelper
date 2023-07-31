namespace StudentJobHelperSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Common.EntityValidationConstants.Employer;
    public class Employer
    {
        public Employer()
        {
            this.Id = Guid.NewGuid();
            this.JobPositions = new HashSet<JobAds>();
        }

        public Guid Id { get; set; }

        [Required]
        [MaxLength(CompanyNumberMaxLength)]
        public string CompanyNumber { get; set; } = null!;

        public Guid UserId { get; set; }

        public virtual ApplicationUser User { get; set; } = null!;

        public ICollection<JobAds> JobPositions { get; set; }

    }
}
