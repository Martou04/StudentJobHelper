using StudentJobHelperSystem.Web.ViewModels.Employer;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace StudentJobHelperSystem.Web.ViewModels.JobAd
{
    public class JobAdDetailsViewModel : JobAdAllViewModel
    {
        public string Description { get; set; } = null!;

        public string Category { get; set; } = null!;

        public string TypeOfEmployment { get; set; } = null!;

        public int OffDaysCount { get; set; }

        public string ForeignLanguage { get; set; } = null!;

        public string Email { get; set; } = null!;

        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; } = null!;
    }
}
