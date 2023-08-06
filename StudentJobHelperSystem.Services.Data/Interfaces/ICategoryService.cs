namespace StudentJobHelperSystem.Services.Data.Interfaces
{
    using StudentJobHelperSystem.Web.ViewModels.Category;
    public interface ICategoryService
    {
        Task<IEnumerable<JobAdSelectCategoryFormModel>> AllCategories();

        Task<bool> ExistsById(int id);

        Task<IEnumerable<string>> AllCategoryName();
    }
}
