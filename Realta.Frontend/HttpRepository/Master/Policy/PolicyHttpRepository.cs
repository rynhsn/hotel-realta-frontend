using Realta.Contract.Models;
using System.Text.Json;

namespace Realta.Frontend.HttpRepository.Master.Policy
{

    public class PolicyHttpRepository : IPolicyHttpRepository
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;

        public PolicyHttpRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<List<PolicyDto>> GetPolicy()
        {

            //AsyncCallback api end point e.g https://localhost:7068/api/policy
            var response = await _httpClient.GetAsync("policy");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var policy = JsonSerializer.Deserialize<List<PolicyDto>>(content, _options);
            return policy;
        }
    }
}
