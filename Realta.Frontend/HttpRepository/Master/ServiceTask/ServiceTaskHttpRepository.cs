using Realta.Contract.Models;
using System.Runtime.Serialization.Json;
using System.Text.Json;

namespace Realta.Frontend.HttpRepository.Master.ServiceTask
{
    public class ServiceTaskHttpRepository : IServiceTaskHttpRepository
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;

        public ServiceTaskHttpRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<List<ServiceTaskDto>> GetServiceTask()
        {

            //AsyncCallback api end point e.g https://localhost:7068/api/servicetask
            var response = await _httpClient.GetAsync("servicetask");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var servicetask = JsonSerializer.Deserialize<List<ServiceTaskDto>>(content, _options);
            return servicetask;
        }
    }
}
