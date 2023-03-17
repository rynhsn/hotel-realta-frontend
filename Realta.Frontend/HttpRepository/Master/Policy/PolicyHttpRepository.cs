using Realta.Contract.Models;
using System.Text.Json;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.Features;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

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

        public async Task<PagingResponse<PolicyDto>> GetPolicyPaging(PolicyParameter policyParameter)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = policyParameter.PageNumber.ToString(),
                ["searchTerm"] = policyParameter.SearchTerm == null ? "" :policyParameter.SearchTerm
            };
            var response =
                await _httpClient.GetAsync(QueryHelpers.AddQueryString("policy/pageList", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var pagingRespone = new PagingResponse<PolicyDto>
            {
                Items = JsonSerializer.Deserialize<List<PolicyDto>>(content, _options),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(),
                    _options)
            };
            Console.WriteLine(pagingRespone.Items);
            return pagingRespone;
        }

        public async Task CreatePolicy(PolicyCreateDto policyCreateDto)
        {
            var content = JsonSerializer.Serialize(policyCreateDto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var postResult = await _httpClient.PostAsync("policy", bodyContent);
            var postContent = await postResult.Content.ReadAsStringAsync();

            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }
        }

        public async Task UpdatePolicy(PolicyDto policyDto)
        {
            var content = JsonSerializer.Serialize(policyDto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var url = Path.Combine("policy", policyDto.PoliId.ToString());

            var postResult = await _httpClient.PutAsync(url, bodyContent);
            var postContent = await postResult.Content.ReadAsStringAsync();


            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }
        }

        public async Task<PolicyDto> GetPolicyById(int id)
        {
            var url = Path.Combine("policy", id.ToString());

            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var policy = JsonSerializer.Deserialize<PolicyDto>(content, _options);
            return policy;
        }

        public async Task DeletePolicy(int id)
        {
            var url = Path.Combine("policy", id.ToString());

            var deleteResult = await _httpClient.DeleteAsync(url);
            var deleteContent = await deleteResult.Content.ReadAsStringAsync();

            if (!deleteResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(deleteContent);
            }
        }
    }
}
