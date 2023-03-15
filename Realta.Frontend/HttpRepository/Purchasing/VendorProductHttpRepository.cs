using System.Text.Json;
using Realta.Contract.Models;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.Features;

namespace Realta.Frontend.HttpRepository.Purchasing;

public class VendorProductHttpRepository : IVendorProductHttpRepository
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _options;

    public VendorProductHttpRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }
    public async  Task<List<VendorProductDto>> GetVendorProduct(int id)
    {
        var response = await _httpClient.GetAsync($"vendorproduct/{id}");
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
        var venpro = JsonSerializer.Deserialize<List<VendorProductDto>>(content, _options); //untuk inject filenya 

        return venpro;
    }

    // public Task<PagingResponse<VendorProductDto>> GetVenProPaging(VendorParameters vendorsParameters, int id)
    // {
    //     throw new NotImplementedException();
    // }
}