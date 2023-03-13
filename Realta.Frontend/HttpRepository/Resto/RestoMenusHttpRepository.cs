using Realta.Contract.Models;
using System.Text.Json;

namespace Realta.Frontend.HttpRepository.Resto
{
    public class RestoMenusHttpRepository : IRestoMenusHttpRepository
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;

        public RestoMenusHttpRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<List<RestoMenusDto>> GetRestoMenus()
        {
            var response = await _httpClient.GetAsync("RestoMenus");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var restoMenus = JsonSerializer.Deserialize<List<RestoMenusDto>>(content, _options); //untuk inject filenya 

            return restoMenus;
        }
    }
}
