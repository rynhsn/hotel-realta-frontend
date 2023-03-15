using System.Text.Json;
using Realta.Contract.Models;

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
}