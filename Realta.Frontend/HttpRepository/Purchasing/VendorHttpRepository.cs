using Realta.Contract.Models;
using System.Text.Json;

namespace Realta.Frontend.HttpRepository.Purchasing
{
    public class VendorHttpRepository : IVendorHttpRepository
    {

        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;

        public VendorHttpRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }
        public async Task<List<VendorDto>> GetVendors()
        {
            var response = await _httpClient.GetAsync("vendor");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var vendors = JsonSerializer.Deserialize<List<VendorDto>>(content, _options); //untuk inject filenya 

            return vendors;
        }
    }
}
