namespace StudentJobHelperSystem.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using StudentJobHelperSystem.Data;

    using Interfaces;
    using Web.ViewModels.Home;

    public class JobAdService : IJobAdService
    {
        private readonly ApplicationDbContext dbContext;

        public JobAdService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<IndexViewModel>> LastThreeJobAdAsync()
        {
            IEnumerable<IndexViewModel> lastThreeJobAds = await this.dbContext
                .JobAds
                .OrderByDescending(j => j.CreatedOn)
                .Take(3)
                .Select(j => new IndexViewModel
                {
                    Id = j.Id.ToString(),
                    Title = j.Title,
                    ImageUrl  = j.LogoUrl
                })
                .ToArrayAsync();

            return lastThreeJobAds;
        }
    }
}
