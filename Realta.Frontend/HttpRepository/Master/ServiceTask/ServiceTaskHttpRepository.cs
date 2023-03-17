using Realta.Contract.Models;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.Json;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.Features;
using Microsoft.AspNetCore.WebUtilities;

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

        public async Task<PagingResponse<ServiceTaskDto>> GetServiceTaskPaging(ServiceTaskParameter serviceTaskParameter)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = serviceTaskParameter.PageNumber.ToString(),
                ["searchTerm"] = serviceTaskParameter.SearchTerm == null ? "" :serviceTaskParameter.SearchTerm
            };
            var response =
                await _httpClient.GetAsync(QueryHelpers.AddQueryString("servicetask/pageList", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var pagingRespone = new PagingResponse<ServiceTaskDto>
            {
                Items = JsonSerializer.Deserialize<List<ServiceTaskDto>>(content, _options),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(),
                    _options)
            };
            Console.WriteLine(pagingRespone.Items);
            return pagingRespone;
        }

        public async Task CreateServiceTask(ServiceTaskCreateDto serviceTaskCreateDto)
        {
            var content = JsonSerializer.Serialize(serviceTaskCreateDto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var postResult = await _httpClient.PostAsync("servicetask", bodyContent);
            var postContent = await postResult.Content.ReadAsStringAsync();

            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }
        }

        public async Task UpdateServiceTask(ServiceTaskDto serviceTaskDto)
        {
            var content = JsonSerializer.Serialize(serviceTaskDto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var url = Path.Combine("servicetask", serviceTaskDto.SetaId.ToString());

            var postResult = await _httpClient.PutAsync(url, bodyContent);
            var postContent = await postResult.Content.ReadAsStringAsync();


            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }

        }

        public async Task<ServiceTaskDto> GetServiceTaskById(int id)
        {
            var url = Path.Combine("servicetask", id.ToString());

            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var seta = JsonSerializer.Deserialize<ServiceTaskDto>(content, _options);
            return seta;
        }

        public async Task deleteServiceTask(int id)
        {
            var url = Path.Combine("servicetask", id.ToString());

            var deleteResult = await _httpClient.DeleteAsync(url);
            var deleteContent = await deleteResult.Content.ReadAsStringAsync();

            if (!deleteResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(deleteContent);
            }
        }
    }
}
