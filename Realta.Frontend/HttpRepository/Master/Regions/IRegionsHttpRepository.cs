using Realta.Contract.Models;

namespace Realta.Frontend.HttpRepository.Master
{
    public interface IRegionsHttpRepository
    {
        Task<List<RegionsDto>> GetRegions();

    }
}
