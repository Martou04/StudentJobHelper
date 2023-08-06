using System.ComponentModel.DataAnnotations;

namespace StudentJobHelperSystem.Web.ViewModels.JobAd
{
    public class JobAdAllViewModel
    {
        public string Id { get; set; } = null!;

        public string Title { get; set; } = null!;

        [Display(Name = "City of workplace")]
        public string CityOfWork { get; set; } = null!;

        [Display(Name = "Logo image link")]
        public string LogoUrl { get; set; } = null!;

        public decimal Salary { get; set; }

        [Display(Name = "Home office")]
        public bool HomeOffice { get; set; } 
    }
}
