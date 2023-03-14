using Realta.Contract.Models;
using Realta.Domain.Entities;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.Features;

namespace Realta.Frontend.HttpRepository.Purchasing;

public interface IPurchaseOrderHttpRepository
{
    Task<List<PurchaseOrderDto>> Get();
    Task<PagingResponse<PurchaseOrderDto>> GetPaging(PurchaseOrderParameters purchaseOrderParameters);
    Task Create(PurchaseOrderTransfer data);
}