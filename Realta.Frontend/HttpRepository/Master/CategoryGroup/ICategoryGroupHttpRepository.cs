using Realta.Contract.Models;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.Features;

namespace Realta.Frontend.HttpRepository.Master.CategoryGroup
{
    public interface ICategoryGroupHttpRepository
    {
        Task<List<CategoryGroupDto>> GetCategoryGroup();

        Task<PagingResponse<CategoryGroupDto>> GetCategoryGroupPaging(CategoryGroupParameter categoryGroupParameter);
    }
}
