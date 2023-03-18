using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.WebUtilities;
using Realta.Contract.Models;

namespace Realta.Frontend.HttpRepository.Purchasing;

public class CartHttpRepository : ICartHttpRepository
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _options;

    public CartHttpRepository( HttpClient httpClient)
    {
        _httpClient = httpClient;
        _options =  new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<List<CartDto>> Get(int empId)
    {
        var queryStringParam = new Dictionary<string, string>
        {
            ["empId"] = empId.ToString()
        };

        var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("cart", queryStringParam));
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }

        var items = JsonSerializer.Deserialize<List<CartDto>>(content, _options); //untuk inject filenya 

        return items;
    }

    public async Task Create(CartDto data)
    {
        var content = JsonSerializer.Serialize(data);
        var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

        var postResult = await _httpClient.PostAsync("cart", bodyContent);
        var postContent = await postResult.Content.ReadAsStringAsync();

        if (!postResult.IsSuccessStatusCode)
        {
            throw new ApplicationException(postContent);
        }
    }

    public async Task Update(CartDto data)
    {
        var content = JsonSerializer.Serialize(data);
        var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

        var postResult = await _httpClient.PutAsync($"cart/{data.CartId}", bodyContent);
        var postContent = await postResult.Content.ReadAsStringAsync();

        if (!postResult.IsSuccessStatusCode)
        {
            throw new ApplicationException(postContent);
        }
    }

    public async Task Delete(CartDto data)
    {
        var url = Path.Combine("cart", data.CartId.ToString());
        var deleteResult = await _httpClient.DeleteAsync(url);
        var deleteContent = await deleteResult.Content.ReadAsStringAsync();

        if (!deleteResult.IsSuccessStatusCode)
        {
            throw new ApplicationException(deleteContent);
        }
    }
}