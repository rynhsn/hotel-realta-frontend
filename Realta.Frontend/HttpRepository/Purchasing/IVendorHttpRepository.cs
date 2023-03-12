using Realta.Contract.Models;

namespace Realta.Frontend.HttpRepository.Purchasing
{
    public interface IVendorHttpRepository
    {
        Task<List<VendorDto>> GetVendors();
    }
}
