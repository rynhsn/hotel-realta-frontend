using Realta.Contract.Models;
using System.Text.Json;

namespace Realta.Frontend.HttpRepository.Master
{
    public class ProvincesHttpRepository : IProvincesHttpRepository
    {

        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;

        public ProvincesHttpRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<List<ProvincesDto>> GetProvinces()
        {

            //AsyncCallback api end point e.g https://localhost:7068/api/provinces
            var response = await _httpClient.GetAsync("provinces");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var provinces = JsonSerializer.Deserialize<List<ProvincesDto>>(content, _options);
            return provinces;
        }

    }
}
