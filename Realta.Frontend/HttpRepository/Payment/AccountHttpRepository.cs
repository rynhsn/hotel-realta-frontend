using System.Net.Http.Json;
using System.Text.Json;
using HotelRealtaPayment.Contract.Models;
using HotelRealtaPayment.Contract.Models.FrontEnd;
using HotelRealtaPayment.Domain.Dto;
using HotelRealtaPayment.Domain.RequestFeatures;
using Microsoft.AspNetCore.WebUtilities;
using Realta.Frontend.Features;

namespace Realta.Frontend.HttpRepository.Payment;

public class AccountHttpRepository : IAccountHttpRepository
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _options;

    public AccountHttpRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<List<AccountDto>> GetAccounts()
    {
        var response = await _httpClient.GetAsync("accounts");
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }

        var result = JsonSerializer.Deserialize<JsonCollection<AccountDto>>(content, _options);

        return result.data["accounts"];
    }
    
    public async Task<List<AccountUser>> GetAccountInfo(int id)
    {
        var response = await _httpClient.GetAsync($"accounts/users/{id}");
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }

        var result = JsonSerializer.Deserialize<JsonCollection<AccountUser>>(content, _options);

        return result.data["accounts"];
    }

    public async Task<HttpResponseMessage> TopUpFintech(TransactionTopUpDto topUp)
    {
        var response = await _httpClient.PostAsJsonAsync("transactions/topup", topUp);
        return response;
    }

    public async Task<PagingResponse<AccountDto>> GetAccountsPaging(AccountParameters accountParameters)
    {
        var queryStringParam = new Dictionary<string, string>
        {
            ["pageNumber"] = accountParameters.PageNumber.ToString(),
            ["searchTerm"] = accountParameters.SearchTerm,
            ["orderBy"] = accountParameters.OrderBy,
            ["pageSize"] = accountParameters.PageSize.ToString()
        };

        var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("accounts/pagelist",queryStringParam));
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }

        var pagingResponse = new PagingResponse<AccountDto>
        {
            Items = JsonSerializer.Deserialize<JsonCollection<AccountDto>>(content, _options).data["accounts"],
            MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), _options)
        };

        return pagingResponse;
    }
}