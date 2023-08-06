namespace StudentJobHelperSystem.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using StudentJobHelperSystem.Data;

    using Interfaces;
    using Web.ViewModels.Home;
    using StudentJobHelperSystem.Web.ViewModels.JobAd;
    using StudentJobHelperSystem.Data.Models;
    using System.ComponentModel;
    using StudentJobHelperSystem.Services.Data.Models.JobAd;
    using StudentJobHelperSystem.Web.ViewModels.JobAd.Enums;

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

        public async Task Create(JobAdFormModel formModel, string agentId)
        {
            JobAds jobAd = new JobAds
            {
                Title = formModel.Title,
                Description = formModel.Description,
                Salary = formModel.Salary,
                CityOfWork = formModel.CityOfWork,
                TypeOfEmployment = formModel.TypeOfEmployment,
                ForeignLanguage = formModel.ForeignLanguage,
                OffDaysCount = formModel.OffDaysCount,
                HomeOffice = formModel.HomeOffice,
                PhoneNumber = formModel.PhoneNumber,
                Email = formModel.Email,
                LogoUrl = formModel.LogoUrl,
                CategoryId = formModel.CategoryId,
                EmployerId = Guid.Parse(agentId),
            };

            await this.dbContext.JobAds.AddAsync(jobAd);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<AllJobAdFilteredAndPagedServiceModel> All(AllJobAdQueryModel queryModel)
        {
            IQueryable<JobAds> jobAdsQuery = this.dbContext
                .JobAds
                .AsQueryable();

            if(!string.IsNullOrWhiteSpace(queryModel.Category))
                jobAdsQuery = jobAdsQuery
                    .Where(j => j.Category.Name == queryModel.Category);

            if (!string.IsNullOrEmpty(queryModel.SearchString))
            {
                string wildCard = $"%{queryModel.SearchString.ToLower()}%";

                jobAdsQuery = jobAdsQuery
                    .Where(j => EF.Functions.Like(j.Title, wildCard) ||
                                EF.Functions.Like(j.Description, wildCard) ||
                                EF.Functions.Like(j.CityOfWork, wildCard));
            }

            jobAdsQuery = queryModel.JobAdSorting switch
            {
                JobAdSorting.Newest => jobAdsQuery
                    .OrderByDescending(j => j.CreatedOn),
                JobAdSorting.Oldest => jobAdsQuery
                    .OrderBy(j => j.CreatedOn),
                JobAdSorting.SalaryAscending => jobAdsQuery
                    .OrderBy(j => j.Salary),
                JobAdSorting.SalaryDescending => jobAdsQuery
                    .OrderByDescending(j => j.Salary),
                _ => jobAdsQuery
                .OrderBy(j => j.HomeOffice != false)
                .ThenByDescending(j=>j.CreatedOn)
            };

            IEnumerable<JobAdAllViewModel> allJobAds = await jobAdsQuery
                .Skip((queryModel.CurrentPage - 1) * queryModel.JobAdsPerPage)
                .Take(queryModel.JobAdsPerPage)
                .Select(j=> new JobAdAllViewModel 
                {
                    Id = j.Id.ToString(),
                    Title = j.Title,
                    CityOfWork = j.CityOfWork,
                    LogoUrl = j.LogoUrl,
                    Salary = j.Salary,
                    HomeOffice = j.HomeOffice
                })
                .ToArrayAsync();

            int totalJobAds = jobAdsQuery.Count();

            return new AllJobAdFilteredAndPagedServiceModel()
            {
                TotalJobAdCount = totalJobAds,
                jobAds = allJobAds
            };
        }
    }
}
