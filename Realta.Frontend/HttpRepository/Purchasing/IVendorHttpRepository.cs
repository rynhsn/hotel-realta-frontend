using Realta.Contract.Models;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.Features;

namespace Realta.Frontend.HttpRepository.Purchasing
{
    public interface IVendorHttpRepository
    {
        Task<List<VendorDto>> GetVendors();
        Task<PagingResponse<VendorDto>> GetVendorPaging(VendorParameters vendorsParameters);
    }
}
