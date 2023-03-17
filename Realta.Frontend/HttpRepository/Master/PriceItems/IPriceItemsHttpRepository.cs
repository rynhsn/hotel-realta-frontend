using Realta.Contract.Models;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.Features;

namespace Realta.Frontend.HttpRepository.Master.PriceItems
{
    public interface IPriceItemsHttpRepository
    {
        Task<List<PriceItemsDto>> GetPriceItems();

        Task<PagingResponse<PriceItemsDto>> GetPriceItemsPaging(PriceItemsParameters priceItemsParameters);

        Task CreatePriceItems(PriceItemsCreateDto priceItemsCreateDto);
        Task UpdatePriceItems(PriceItemsDto priceItemsDto);
        Task<PriceItemsDto> GetPriceItemsById(int id);

        Task deletePriceItems(int id);
    }
}
