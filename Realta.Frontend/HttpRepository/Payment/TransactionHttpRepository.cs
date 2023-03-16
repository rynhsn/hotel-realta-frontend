using System.Text.Json;
using HotelRealtaPayment.Contract.Models;
using HotelRealtaPayment.Contract.Models.FrontEnd;
using HotelRealtaPayment.Domain.RequestFeatures;
using Microsoft.AspNetCore.WebUtilities;
using Realta.Frontend.Features;

namespace Realta.Frontend.HttpRepository.Payment;

public class TransactionHttpRepository : ITransactionHttpRepository
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _options;

    public TransactionHttpRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<List<TransactionDto>> GetTransactions()
    {
        var response = await _httpClient.GetAsync("transactions");
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }

        var result = JsonSerializer.Deserialize<JsonCollection<TransactionDto>>(content, _options);

        return result.data["transactions"];
    }
    
    public async Task<PagingResponse<TransactionDto>> GetTransactionPaging(TransactionParameters transactionParameters)
    {
        var queryStringParam = new Dictionary<string, string>
        {
            ["pageNumber"] = transactionParameters.PageNumber.ToString(),
            ["searchTerm"] = transactionParameters.SearchTerm,
            ["orderBy"] = transactionParameters.OrderBy,
            ["pageSize"] = transactionParameters.PageSize.ToString(),
            ["type"] = transactionParameters.Type
        };


        var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("transactions/pagelist",queryStringParam));
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }

        var pagingResponse = new PagingResponse<TransactionDto>
        {
            Items = JsonSerializer.Deserialize<JsonCollection<TransactionDto>>(content, _options).data["transactions"],
            MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), _options)
        };

        return pagingResponse;
    }
}