using Realta.Contract.Models;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.Features;


namespace Realta.Frontend.HttpRepository.Purchasing;

public interface IVendorProductHttpRepository
{
   Task<PagingResponse<VendorProductDto>> GetVenProPaging(VenproParameters vendorsParameters, int id);
   Task DeleteVenpro(int id);
   Task CreateVenpro(VendorProductDto venproCreate);
   Task<VendorHeaderDto> GetHeaderId(int id);
   Task<List<VendorProductDto>> GetStock();
}