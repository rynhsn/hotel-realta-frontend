using Realta.Contract.Models;
using System.Runtime.Serialization.Json;
using System.Text.Json;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.Features;
using Microsoft.AspNetCore.WebUtilities;

namespace Realta.Frontend.HttpRepository.Master.CategoryGroup
{
    public class CategoryGroupHttpRepository : ICategoryGroupHttpRepository
    {

        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;

        public CategoryGroupHttpRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<List<CategoryGroupDto>> GetCategoryGroup()
        {

            //AsyncCallback api end point e.g https://localhost:7068/api/categorygroup
            var response = await _httpClient.GetAsync("categorygroup");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);    
            }

            var cagro = JsonSerializer.Deserialize<List<CategoryGroupDto>>(content, _options);
            return cagro;
        }

        public async Task<PagingResponse<CategoryGroupDto>> GetCategoryGroupPaging(CategoryGroupParameter categoryGroupParameter)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = categoryGroupParameter.PageNumber.ToString(),
                ["searchTerm"] = categoryGroupParameter.SearchTerm == null ? "" :categoryGroupParameter.SearchTerm
            };
            var response =
                await _httpClient.GetAsync(QueryHelpers.AddQueryString("categorygroup/pageList", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var pagingRespone = new PagingResponse<CategoryGroupDto>
            {
                Items = JsonSerializer.Deserialize<List<CategoryGroupDto>>(content, _options),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(),
                    _options)
            };
            Console.WriteLine(pagingRespone.Items);
            return pagingRespone;
        }
    }
}
