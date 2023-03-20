using Realta.Contract.Models;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.Features;

namespace Realta.Frontend.HttpRepository.Purchasing;

public interface IStockDetailHttpRepository
{
    Task<List<StockDetailDto>> GetStockDetail(int id);
    Task<PagingResponse<StockDetailDto>> GetStockDetailPaging(StockDetailParameters stocksDetailParameters);
    Task<StockDetailDto> GetStockDetailById(int id);
    Task EditStatus(StockDetailDto stockDetailDto);
    Task GenerateBarcode(QtyUpdateDto GenerateBarcodePd);

}

