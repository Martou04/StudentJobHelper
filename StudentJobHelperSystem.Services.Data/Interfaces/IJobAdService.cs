

namespace StudentJobHelperSystem.Services.Data.Interfaces
{
    using Web.ViewModels.Home;
    public interface IJobAdService
    {
        Task<IEnumerable<IndexViewModel>> LastThreeJobAdAsync();

    }
}
