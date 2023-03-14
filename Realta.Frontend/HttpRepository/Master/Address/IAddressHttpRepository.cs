using Realta.Contract.Models;

namespace Realta.Frontend.HttpRepository.Master
{
    public interface IAddressHttpRepository
    {
        Task<List<AddressDto>> GetAddress();
    }
}
