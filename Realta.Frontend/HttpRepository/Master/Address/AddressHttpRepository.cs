using Realta.Contract.Models;
using System.Text.Json;

namespace Realta.Frontend.HttpRepository.Master
{
        public class AddressHttpRepository : IAddressHttpRepository
    {

        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;

        public AddressHttpRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }
        public async Task<List<AddressDto>> GetAddress()
        {

            //AsyncCallback api end point e.g https://localhost:7068/api/address
            var response = await _httpClient.GetAsync("address");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var address = JsonSerializer.Deserialize<List<AddressDto>>(content, _options);
            return address;
        }

    }
}
