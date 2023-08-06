namespace StudentJobHelperSystem.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using StudentJobHelperSystem.Data;

    using Interfaces;
    using Web.ViewModels.Home;
    using StudentJobHelperSystem.Web.ViewModels.JobAd;
    using StudentJobHelperSystem.Data.Models;
    using System.ComponentModel;

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
    }
}
