using System.Text.Json;
using Realta.Contract.Models.v1.Hotels;

namespace Realta.Frontend.HttpRepository.Hotel
{
    public class HotelsHttpRepository : IHotelsHttpRepository
    {

        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;


        public HotelsHttpRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<List<HotelsDto>> GetHotels()
        {
            //Call api end point, e.g :https://localhost:7068/api/hotels 
            var response = await _httpClient.GetAsync("hotels");
            var content = await response.Content.ReadAsStringAsync();


            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var hotels = JsonSerializer.Deserialize<List<HotelsDto>>(content, _options);
            return hotels;

        }
    }
}