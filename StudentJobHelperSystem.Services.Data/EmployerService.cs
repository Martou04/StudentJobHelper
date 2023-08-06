using Microsoft.EntityFrameworkCore;
using StudentJobHelperSystem.Data;
using StudentJobHelperSystem.Data.Models;
using StudentJobHelperSystem.Services.Data.Interfaces;
using StudentJobHelperSystem.Web.ViewModels.Employer;

namespace StudentJobHelperSystem.Services.Data
{
    public class EmployerService : IEmployerService
    {
        private readonly ApplicationDbContext dbContext;

        public EmployerService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> EmployerExistByCompanyNumber(string companyNumber)
        {
            bool result = await this.dbContext
                .Employers
                .AnyAsync(e => e.CompanyNumber == companyNumber);

            return result;
        }

        public async Task<bool> EmployerExistByUserId(string userId)
        {
            bool result = await this.dbContext
                .Employers
                .AnyAsync(e => e.UserId.ToString() == userId);

            return result;
        }

        public async Task<bool> UserHasWorkplace(string userId)
        {
            ApplicationUser? user = await this.dbContext
                .Users
                .FirstOrDefaultAsync(u => u.Id.ToString() == userId);

            if (user == null)
                return false;

            return user.WorkPlaces.Any();
        }

        public async Task Create(string userId, BecomeEmployerFormModel model)
        {
            Employer employer = new Employer()
            {
                UserId = Guid.Parse(userId),
                CompanyNumber = model.CompanyNumber
            };

            await this.dbContext.Employers.AddAsync(employer);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<string?> GetEmployerIdByUserId(string userId)
        {
            Employer? employer = await this.dbContext
                .Employers
                .FirstOrDefaultAsync(e => e.UserId.ToString() == userId);
            if(employer == null)
                return null;

            return employer.Id.ToString();
        }
    }
}
