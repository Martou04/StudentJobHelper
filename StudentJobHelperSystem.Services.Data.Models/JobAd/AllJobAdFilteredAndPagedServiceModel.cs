namespace StudentJobHelperSystem.Services.Data.Models.JobAd
{
    using StudentJobHelperSystem.Web.ViewModels.JobAd;

    public class AllJobAdFilteredAndPagedServiceModel
    {
        public AllJobAdFilteredAndPagedServiceModel()
        {
            this.jobAds = new HashSet<JobAdAllViewModel>();
        }

        public int TotalJobAdCount { get; set; }

        public IEnumerable<JobAdAllViewModel> jobAds { get; set; }
    }
}
