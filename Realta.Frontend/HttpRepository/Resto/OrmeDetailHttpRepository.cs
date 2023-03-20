using Realta.Contract.Models;
using Realta.Domain.Dto;
using System.Text;
using System.Text.Json;

namespace Realta.Frontend.HttpRepository.Resto
{
    public class OrmeDetailHttpRepository : IOrmeDetailHttpRepository
    {

        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;
        public OrmeDetailHttpRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task CreateDetail(NewOrderMenusDto ormeDetailDto)
        {
            var content = JsonSerializer.Serialize(ormeDetailDto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var postResult = await _httpClient.PostAsync("OrmeDetail", bodyContent);
            var postContent = await postResult.Content.ReadAsStringAsync();


            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }
        }

        public Task CreateDetail(OrderMenusDto ormeDetailDto)
        {
            throw new NotImplementedException();
        }

        public async Task<List<OrderMenusDto>> GetOrderMenus()
        {
            var response = await _httpClient.GetAsync("OrderMenus");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var ormeDetail = JsonSerializer.Deserialize<List<OrderMenusDto>>(content, _options); //untuk inject filenya 

            return ormeDetail;
        }

        public async Task UpdateDetail(OrderMenusDto ormeDetailDto)
        {
            var content = JsonSerializer.Serialize(ormeDetailDto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var url = Path.Combine("OrmeDetail/", ormeDetailDto.OrmeId.ToString());

            var postResult = await _httpClient.PutAsync(url, bodyContent);
            var postContent = await postResult.Content.ReadAsStringAsync();

            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }
        }
    }
}
