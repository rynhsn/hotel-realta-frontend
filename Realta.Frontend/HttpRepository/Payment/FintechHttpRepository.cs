using System.Net.Http.Json;
using System.Text.Json;
using HotelRealtaPayment.Contract.Models;
using HotelRealtaPayment.Contract.Models.FrontEnd;
using HotelRealtaPayment.Domain.RequestFeatures;
using Microsoft.AspNetCore.WebUtilities;
using Realta.Frontend.Features;

namespace Realta.Frontend.HttpRepository.Payment;

public class FintechHttpRepository : IFintechHttpRepository
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _options;

    public FintechHttpRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<List<FintechDto>> GetFintechs()
    {
        var response = await _httpClient.GetAsync("fintechs");
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }

        var result = JsonSerializer.Deserialize<JsonCollection<FintechDto>>(content, _options);

        return result.data["fintechs"];
    }

    public async Task<PagingResponse<FintechDto>> GetFintechsPaging(FintechParameters fintechParameters)
    {
        var queryStringParam = new Dictionary<string, string>
        {
            ["pageNumber"] = fintechParameters.PageNumber.ToString(),
            ["searchTerm"] = fintechParameters.SearchTerm,
            ["orderBy"] = fintechParameters.OrderBy,
            ["pageSize"] = fintechParameters.PageSize.ToString()
        };

        var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("fintechs/pagelist",queryStringParam));
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }

        var pagingResponse = new PagingResponse<FintechDto>
        {
            Items = JsonSerializer.Deserialize<JsonCollection<FintechDto>>(content, _options).data["fintechs"],
            MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), _options)
        };

        return pagingResponse;
    }

    public async Task Update(FintechDto fintech)
    {
        var response = await _httpClient.PutAsJsonAsync($"fintechs/{fintech.Id}", fintech);
        var content = await response.Content.ReadAsStringAsync();
        
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
    }

    public async Task Create(FintechDto fintech)
    {
        var response = await _httpClient.PostAsJsonAsync("fintechs", fintech);
        var content = await response.Content.ReadAsStringAsync();
        
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
    }

    public async Task Delete(int id)
    {
        var response = await _httpClient.DeleteAsync($"fintechs/{id}");
        var content = await response.Content.ReadAsStringAsync();
        
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
    }
}