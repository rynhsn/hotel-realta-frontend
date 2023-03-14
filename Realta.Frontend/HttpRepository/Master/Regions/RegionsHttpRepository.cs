using Realta.Contract.Models;
using System.Text.Json;

namespace Realta.Frontend.HttpRepository.Master
{
    public class RegionsHttpRepository : IRegionsHttpRepository
    {

        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;

        public RegionsHttpRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<List<RegionsDto>> GetRegions()
        {

            //AsyncCallback api end point e.g https://localhost:7068/api/regions
            var response = await _httpClient.GetAsync("regions");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var region = JsonSerializer.Deserialize<List<RegionsDto>>(content, _options);
            return region;
        }




    }
}
