using System.Text.Json;
using Realta.Contract.Models;

namespace Realta.Frontend.HttpRepository;

public class CartHttpRepository:ICartHttpRepository
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _options;

    public CartHttpRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _options = new JsonSerializerOptions{ PropertyNameCaseInsensitive = true }
        ;
    }

    public async Task<List<CartDto>?> GetCarts()
    {
        //call api endpoint, eg: https://localhost:7068/api/cart
        var response = await _httpClient.GetAsync("cart");
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }

        var carts = JsonSerializer.Deserialize<List<CartDto>>(content, _options);
        return carts;
    }
}