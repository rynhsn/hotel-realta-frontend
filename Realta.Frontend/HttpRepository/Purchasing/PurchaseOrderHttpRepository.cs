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
    
    // public async Task<PagingResponse<PurchaseOrderDto>> GetPaging(PurchaseOrderParameters purchaseOrderParameters)
    // {
    //     var queryStringParam = new Dictionary<string, string>
    //     {
    //         ["pageNumber"] = purchaseOrderParameters.PageNumber.ToString()
    //     };
    //     
    //     var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("purchaseorder",queryStringParam));
    //     var content = await response.Content.ReadAsStringAsync();
    //     
    //     if (!response.IsSuccessStatusCode)
    //     {
    //         throw new ApplicationException(content);
    //     }
    //
    //     var pagingResponse = new PagingResponse<PurchaseOrderDto>
    //     {
    //         Items = JsonSerializer.Deserialize<List<PurchaseOrderDto>>(content, _options),
    //         MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), _options)
    //     };
    //
    //     return pagingResponse;
    // }
    
    public async Task<PagingResponse<PurchaseOrderDto>> GetPaging(PurchaseOrderParameters purchaseOrderParameters)
    {
        var queryStringParam = new Dictionary<string, string>
        {
            ["pageNumber"] = purchaseOrderParameters.PageNumber.ToString()
        };

        var uri = QueryHelpers.AddQueryString("purchaseorder", queryStringParam);
        var response = await _httpClient.GetAsync(uri);
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }

        var pagingResponse = new PagingResponse<PurchaseOrderDto>
        {
            Items = JsonSerializer.Deserialize<List<PurchaseOrderDto>>(content, _options)
        };

        if (response.Headers.TryGetValues("X-Pagination", out var values))
        {
            var metaDataJson = values.First();
            var metaData = JsonSerializer.Deserialize<MetaData>(metaDataJson, _options);
            pagingResponse.MetaData = metaData;
        }

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


