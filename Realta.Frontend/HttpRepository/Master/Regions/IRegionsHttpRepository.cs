using Realta.Contract.Models;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.Features;

namespace Realta.Frontend.HttpRepository.Master
{
    public interface IRegionsHttpRepository
    {
        Task<List<RegionsDto>> GetRegions();
        Task<PagingResponse<RegionsDto>> GetRegionsPaging(RegionsParameter regionsParameter);

    }
}
