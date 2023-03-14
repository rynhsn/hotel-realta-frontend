using Realta.Contract.Models;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.Features;

namespace Realta.Frontend.HttpRepository.Master.PriceItems
{
    public interface IPriceItemsHttpRepository
    {
        Task<List<PriceItemsDto>> GetPriceItems();

        Task<PagingResponse<PriceItemsDto>> GetPriceItemsPaging(PriceItemsParameters priceItemsParameters);
    }
}
