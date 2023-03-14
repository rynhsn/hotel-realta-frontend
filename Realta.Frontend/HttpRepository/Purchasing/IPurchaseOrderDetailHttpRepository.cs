using Realta.Domain.Entities;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.Features;

namespace Realta.Frontend.HttpRepository.Purchasing;

public interface IPurchaseOrderDetailHttpRepository
{
    Task<List<PurchaseOrderTransfer>> Get();
    Task<PagingResponse<PurchaseOrderTransfer>> GetPaging(PurchaseOrderParameters purchaseOrderParameters);
}