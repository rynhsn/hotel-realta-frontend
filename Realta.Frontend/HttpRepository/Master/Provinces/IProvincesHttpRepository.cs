using Realta.Contract.Models;

namespace Realta.Frontend.HttpRepository.Master
{
    public interface IProvincesHttpRepository
    {
        Task<List<ProvincesDto>> GetProvinces();
    }
}
