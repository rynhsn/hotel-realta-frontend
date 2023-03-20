using Realta.Contract.Models;
using System.Text.Json;

namespace Realta.Frontend.HttpRepository.Resto
{
    public class OrderMenusHttpRepository : IOrderMenusHttpRepository
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;

        public OrderMenusHttpRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }
        public async  Task<List<OrderMenusDto>> GetOrderMenus()
        {
            var response = await _httpClient.GetAsync("OrderMenus");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var orderMenus = JsonSerializer.Deserialize<List<OrderMenusDto>>(content, _options); //untuk inject filenya 

            return orderMenus;
        }
    }
}
