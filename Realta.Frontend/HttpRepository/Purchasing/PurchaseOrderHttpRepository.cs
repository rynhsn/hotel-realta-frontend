using System.Text.Json;
using Microsoft.AspNetCore.WebUtilities;
using Realta.Contract.Models;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.Features;

namespace Realta.Frontend.HttpRepository.Purchasing;

public class PurchaseOrderHttpRepository : IPurchaseOrderHttpRepository
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _options;

    public PurchaseOrderHttpRepository( HttpClient httpClient)
    {
        _httpClient = httpClient;
        _options =  new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }
    
    public async Task<PagingResponse<PurchaseOrderDto>> GetPaging(PurchaseOrderParameters parameters)
    {
        var queryStringParam = new Dictionary<string, string>
        {
            ["pageNumber"] = parameters.PageNumber.ToString(),
            ["keyword"] = parameters.Keyword == null ? "": parameters.Keyword
        };

        var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("PurchaseOrder", queryStringParam));
        var content = await response.Content.ReadAsStringAsync();
        
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }

        var pagingResponse = new PagingResponse<PurchaseOrderDto>
        {
            Items = JsonSerializer.Deserialize<List<PurchaseOrderDto>>(content, _options),
            MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), _options)
        };

        return pagingResponse;
    }
    

    public async Task<List<PurchaseOrderDto>> Get()
    {
        var response = await _httpClient.GetAsync("purchaseorder");
        var content = await response.Content.ReadAsStringAsync();
        
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }

        var result = JsonSerializer.Deserialize<List<PurchaseOrderDto>>(content, _options); //untuk inject filenya 

        return result;
    }

}


