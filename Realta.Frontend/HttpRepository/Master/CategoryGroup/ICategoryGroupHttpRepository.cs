using Realta.Contract.Models;

namespace Realta.Frontend.HttpRepository.Master.CategoryGroup
{
    public interface ICategoryGroupHttpRepository
    {
        Task<List<CategoryGroupDto>> GetCategoryGroup();

    }
}
