namespace StudentJobHelperSystem.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using StudentJobHelperSystem.Data;
    using StudentJobHelperSystem.Services.Data.Interfaces;
    using StudentJobHelperSystem.Web.ViewModels.Category;

    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext dbContext;

        public CategoryService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<JobAdSelectCategoryFormModel>> AllCategories()
        {
            IEnumerable<JobAdSelectCategoryFormModel> allCategories = await this.dbContext
                .Categories
                .AsNoTracking()
                .Select(c=>new JobAdSelectCategoryFormModel()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToArrayAsync();

            return allCategories;
        }

        public async Task<bool> ExistsById(int id)
        {
            bool result = await this.dbContext
                .Categories
                .AnyAsync(c => c.Id == id);

            return result;
        }
    }
}
