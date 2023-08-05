using System.ComponentModel.DataAnnotations;

using static StudentJobHelperSystem.Common.EntityValidationConstants.Employer;

namespace StudentJobHelperSystem.Web.ViewModels.Employer
{
    public class BecomeEmployerFormModel
    {
        [Required]
        [StringLength(CompanyNumberMaxLength,MinimumLength = CompanyNumberMinLength)]
        [Display(Name ="Company number")]
        public string CompanyNumber { get; set; } = null!;
    }
}
