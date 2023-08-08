namespace StudentJobHelperSystem.Services.Data.Interfaces
{
    using StudentJobHelperSystem.Services.Data.Models.JobAd;
    using StudentJobHelperSystem.Web.ViewModels.JobAd;
    using Web.ViewModels.Home;
    public interface IJobAdService
    {
        Task<IEnumerable<IndexViewModel>> LastThreeJobAdAsync();

        Task Create(JobAdFormModel formModel, string employerId);

        Task<AllJobAdFilteredAndPagedServiceModel> All(AllJobAdQueryModel queryModel);

        Task<IEnumerable<JobAdAllViewModel>> AllByEmployerId(string employerId);

        Task<IEnumerable<JobAdAllViewModel>> AllByUserId(string userId);

        Task<JobAdDetailsViewModel?> GetDetailsById(string jobAdId);
    }
}
