using System.Text.Json;
using Realta.Contract.Models;

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

        var banks = JsonSerializer.Deserialize<JsonCollection>(content, _options);

        return banks.data["banks"];
    }
}