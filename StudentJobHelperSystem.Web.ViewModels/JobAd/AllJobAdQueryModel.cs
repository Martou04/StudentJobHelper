namespace StudentJobHelperSystem.Web.ViewModels.JobAd
{
    using StudentJobHelperSystem.Web.ViewModels.JobAd.Enums;
    using System.ComponentModel.DataAnnotations;
    using static Common.GeneralApplicationConstants;
    public class AllJobAdQueryModel
    {
        public AllJobAdQueryModel()
        {
            this.CurrentPage = DefaultPage;
            this.JobAdsPerPage = EntitiesPerPage;

            this.Categories = new HashSet<string>();
            this.JobAds = new HashSet<JobAdAllViewModel>();
        }
        public string? Category { get; set; }

        [Display(Name ="Search by word")]
        public string? SearchString { get; set; }

        [Display(Name ="Sort job ad by")]
        public JobAdSorting JobAdSorting { get; set; }

        public int CurrentPage { get; set; }

        [Display(Name ="Offers On Page")]
        public int JobAdsPerPage { get; set; }

        public int TotalJobAds { get; set; }

        public IEnumerable<string> Categories { get; set; } = null!;

        public IEnumerable<JobAdAllViewModel> JobAds { get; set; }
    }
}
