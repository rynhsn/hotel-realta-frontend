using System.Text.Json;
using Microsoft.AspNetCore.WebUtilities;
using Realta.Contract.Models;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.Features;

namespace Realta.Frontend.HttpRepository.Purchasing;

public class StocksHttpRepository : IStocksHttpRepository
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _options;

    public StocksHttpRepository( HttpClient httpClient)
    {
        _httpClient = httpClient;
        _options =  new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<List<StocksDto>> GetStocks()
    {
        // call api end point e.g : http://localhost:7068/api/stocks
        var response = await _httpClient.GetAsync("stocks");
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }

        var stocks = JsonSerializer.Deserialize<List<StocksDto>>(content, _options);
        return stocks;
    }

    public async Task<PagingResponse<StocksDto>> GetStocksPaging(StocksParameters stocksParameters)
    {
        var queryStringParam = new Dictionary<string, string>
        {
            ["pageNumber"] = stocksParameters.PageNumber.ToString(),
            ["searchTerm"] = stocksParameters.SearchTerm == null ? "" : stocksParameters.SearchTerm.ToString(),
        };

        var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("stocks/pageList", queryStringParam));
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }

        var pagingResponse = new PagingResponse<StocksDto>
        {
            Items = JsonSerializer.Deserialize<List<StocksDto>>(content, _options),
            MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), _options)
        };

        return pagingResponse;
    }

    public async Task<List<StockPhotoDto>> GetStocksPhoto(int stockId)
    {
        // call api end point e.g : http://localhost:7068/api/stocks
        string url = "stock_photo" + stockId;
        var response = await _httpClient.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }

        var stockPhoto = JsonSerializer.Deserialize<List<StockPhotoDto>>(content, _options);
        return stockPhoto;
    }
}


