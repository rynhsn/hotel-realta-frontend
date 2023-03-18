using System.Net.Http.Json;
using System.Text.Json;
using HotelRealtaPayment.Contract.Models;
using HotelRealtaPayment.Contract.Models.FrontEnd;
using HotelRealtaPayment.Domain.RequestFeatures;
using Microsoft.AspNetCore.WebUtilities;
using Realta.Frontend.Features;

namespace Realta.Frontend.HttpRepository.Payment;

public class BankHttpRepository : IBankHttpRepository
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _options;

    public BankHttpRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<List<BankDto>> GetBanks()
    {
        var response = await _httpClient.GetAsync("banks");
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }

        var result = JsonSerializer.Deserialize<JsonCollection<BankDto>>(content, _options);

        return result.data["banks"];
    }

    public async Task<PagingResponse<BankDto>> GetBanksPaging(BankParameters bankParameters)
    {
        var queryStringParam = new Dictionary<string, string>
        {
            ["pageNumber"] = bankParameters.PageNumber.ToString(),
            ["searchTerm"] = bankParameters.SearchTerm,
            ["orderBy"] = bankParameters.OrderBy,
            ["pageSize"] = bankParameters.PageSize.ToString()
        };

        var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("banks/pagelist",queryStringParam));
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }

        var pagingResponse = new PagingResponse<BankDto>
        {
            Items = JsonSerializer.Deserialize<JsonCollection<BankDto>>(content, _options).data["banks"],
            MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), _options)
        };

        return pagingResponse;
    }

    public async Task UpdateBank(BankDto bank)
    {
        var response = await _httpClient.PutAsJsonAsync($"banks/{bank.Id}", bank);
        var content = await response.Content.ReadAsStringAsync();   
        
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
    }

    public async Task CreateBank(BankDto bank)
    {
        var response = await _httpClient.PostAsJsonAsync("banks", bank);
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
    }

    public async Task DeleteBank(int id)
    {
        var response = await _httpClient.DeleteAsync($"banks/{id}");
        var content = await response.Content.ReadAsStringAsync();
        
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content.ToString());
        }
    }
}