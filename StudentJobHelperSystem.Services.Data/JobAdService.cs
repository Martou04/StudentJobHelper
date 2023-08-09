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
    using StudentJobHelperSystem.Web.ViewModels.Employer;

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
                .Where(j => j.IsActive)
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

        public async Task<string> CreateAndReturnId(JobAdFormModel formModel, string agentId)
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

            return jobAd.Id.ToString();
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
                .Where(j => j.IsActive == true)
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

        public async Task<IEnumerable<JobAdAllViewModel>> AllByEmployerId(string employerId)
        {
            IEnumerable<JobAdAllViewModel> allEmployerJobAd = await this.dbContext
                .JobAds
                .Where(j => j.IsActive &&
                            j.EmployerId.ToString() == employerId)
                .Select(j => new JobAdAllViewModel()
                {
                    Id = j.Id.ToString(),
                    Title = j.Title,
                    CityOfWork = j.CityOfWork,
                    LogoUrl = j.LogoUrl,
                    Salary = j.Salary,
                    HomeOffice = j.HomeOffice

                })
                .ToArrayAsync();

            return allEmployerJobAd;
        }

        public async Task<IEnumerable<JobAdAllViewModel>> AllByUserId(string userId)
        {
            IEnumerable<JobAdAllViewModel> allEmployeeCandidatesJobAd = await this.dbContext
                .JobAds
                .Where(j => j.IsActive &&
                            j.WorkerId.HasValue &&
                            j.WorkerId.ToString() == userId)
                .Select(j=> new JobAdAllViewModel() 
                {
                    Id = j.Id.ToString(),
                    Title = j.Title,
                    CityOfWork = j.CityOfWork,
                    LogoUrl = j.LogoUrl,
                    Salary = j.Salary,
                    HomeOffice = j.HomeOffice
                })
                .ToArrayAsync();

            return allEmployeeCandidatesJobAd;
        }

        public async Task<JobAdDetailsViewModel> GetDetailsById(string jobAdId)
        {
            JobAds jobAd = await this.dbContext
                .JobAds
                .Include(j => j.Category)
                .Where(j => j.IsActive)
                .FirstAsync(j => j.Id.ToString() == jobAdId);

            return new JobAdDetailsViewModel()
            {
                Title = jobAd.Title,
                Description = jobAd.Description,
                Salary = jobAd.Salary,
                CityOfWork = jobAd.CityOfWork,
                TypeOfEmployment = jobAd.TypeOfEmployment,
                OffDaysCount = jobAd.OffDaysCount,
                ForeignLanguage = jobAd.ForeignLanguage,
                HomeOffice = jobAd.HomeOffice,
                Category = jobAd.Category.Name,
                LogoUrl= jobAd.LogoUrl,
                Email = jobAd.Email,
                PhoneNumber = jobAd.PhoneNumber
            };
            
        }

        public async Task<bool> ExistsById(string jobAdId)
        {
            bool result = await this.dbContext
                .JobAds
                .Where(j => j.IsActive)
                .AnyAsync(j => j.Id.ToString() == jobAdId);

            return result; 
        }

        public async Task<JobAdFormModel> GetJobAdForEditById(string jobAdId)
        {
            JobAds jobAd = await this.dbContext
                .JobAds
                .Include(j => j.Category)
                .Where(j => j.IsActive)
                .FirstAsync(j => j.Id.ToString() == jobAdId);

            return new JobAdFormModel
            {
                Title = jobAd.Title,
                Description = jobAd.Description,
                Salary = jobAd.Salary,
                CityOfWork = jobAd.CityOfWork,
                TypeOfEmployment = jobAd.TypeOfEmployment,
                ForeignLanguage = jobAd.ForeignLanguage,
                OffDaysCount = jobAd.OffDaysCount,
                HomeOffice = jobAd.HomeOffice,
                PhoneNumber = jobAd.PhoneNumber,
                Email = jobAd.Email,
                LogoUrl = jobAd.LogoUrl,
                CategoryId = jobAd.CategoryId
            };
        }

        public async Task<bool> IsEmployerWithIdOwnerOfJobAdWithId(string jobAdId, string employerId)
        {
            JobAds jobAd = await this.dbContext
                .JobAds
                .Where(j => j.IsActive)
                .FirstAsync(j => j.Id.ToString() == jobAdId);

            return jobAd.EmployerId.ToString() == employerId;
        }

        public async Task EditJobAdByIdAndFormModel(string jobAdId, JobAdFormModel formModel)
        {
            JobAds jobAd = await this.dbContext
                .JobAds
                .Where(j => j.IsActive)
                .FirstAsync(j => j.Id.ToString() == jobAdId);

            jobAd.Title = formModel.Title;
            jobAd.Description = formModel.Description;
            jobAd.Salary = formModel.Salary;
            jobAd.CityOfWork = formModel.CityOfWork;
            jobAd.TypeOfEmployment = formModel.TypeOfEmployment;
            jobAd.OffDaysCount = formModel.OffDaysCount;
            jobAd.ForeignLanguage = formModel.ForeignLanguage;
            jobAd.HomeOffice = formModel.HomeOffice;
            jobAd.PhoneNumber = formModel.PhoneNumber;
            jobAd.Email = formModel.Email;
            jobAd.LogoUrl = formModel.LogoUrl;
            jobAd.CategoryId = formModel.CategoryId;

            await this.dbContext.SaveChangesAsync();
        }

        public async Task<JobAdPreDeleteDetailsViewModel> GetJobAdForDeleteById(string jobAdId)
        {
            JobAds jobAd = await this.dbContext
                .JobAds
                .Where(j => j.IsActive)
                .FirstAsync(j => j.Id.ToString() == jobAdId);

            return new JobAdPreDeleteDetailsViewModel()
            {
                Title = jobAd.Title,
                CityOfWork = jobAd.CityOfWork,
                LogoUrl = jobAd.LogoUrl
            };
        }

        public async Task DeleteJobAdById(string jobAdId)
        {
            JobAds jobAdToDelete = await this.dbContext
                .JobAds
                .Where(j=>j.IsActive)
                .FirstAsync(j =>j.Id.ToString() == jobAdId);

            jobAdToDelete.IsActive = false;

            await this.dbContext.SaveChangesAsync();
        }
    }
}
