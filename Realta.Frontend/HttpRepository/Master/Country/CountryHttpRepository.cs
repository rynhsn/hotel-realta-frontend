using Realta.Contract.Models;
using System.Text.Json;

namespace Realta.Frontend.HttpRepository.Master
{
    public class CountryHttpRepository : ICountryHttpRepository
    {

        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;

        public CountryHttpRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<List<CountryDto>> GetCountry()
        {

            //AsyncCallback api end point e.g https://localhost:7068/api/country
            var response = await _httpClient.GetAsync("country");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var country = JsonSerializer.Deserialize<List<CountryDto>>(content, _options);
            return country;
        }
    }
}
