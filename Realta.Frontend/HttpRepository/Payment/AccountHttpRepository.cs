using System.Text.Json;
using HotelRealtaPayment.Contract.Models;
using HotelRealtaPayment.Contract.Models.FrontEnd;

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
}