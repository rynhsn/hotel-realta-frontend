using System.Text.Json;
using Microsoft.AspNetCore.WebUtilities;
using Realta.Contract.Models;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.Features;

namespace Realta.Frontend.HttpRepository.Purchasing;

public class VendorProductHttpRepository : IVendorProductHttpRepository
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _options;

    public VendorProductHttpRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<PagingResponse<VendorProductDto>> GetVenProPaging(VenproParameters _param, int id)
    {
        var queryStringParam = new Dictionary<string, string>
        {
            ["pageNumber"] = _param.PageNumber.ToString(),
            ["Keyword"] = _param.Keyword == null ? "" : _param.Keyword,
            ["orderBy"] = _param.OrderBy
        };
        var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString($"vendorproduct/{id}",queryStringParam));
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
        var pagingResponse = new PagingResponse<VendorProductDto>
        {
            Items = JsonSerializer.Deserialize<List<VendorProductDto>>(content, _options),
            MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), _options)
        };
        return pagingResponse;
    }

    public Task DeleteVenpro(int id)
    {
        throw new NotImplementedException();
    }

    public Task CreateVenpro(VendorProductDto venproCreate)
    {
        throw new NotImplementedException();
    }
}