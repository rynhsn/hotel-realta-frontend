using Realta.Contract.Models;
using Realta.Domain.Entities;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.Features;

namespace Realta.Frontend.HttpRepository.Purchasing;

public interface IPurchaseOrderHttpRepository
{
    Task<List<PurchaseOrderDto>> Get();
    Task<PagingResponse<PurchaseOrderDto>> GetHeaders(PurchaseOrderParameters param);
    Task<PagingResponse<PurchaseOrderDetailDto>> GetDetails(string po, PurchaseOrderDetailParameters param);
    Task Create(PurchaseOrderTransfer data);
}
