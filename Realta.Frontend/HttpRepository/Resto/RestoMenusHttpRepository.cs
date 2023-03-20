using Microsoft.AspNetCore.WebUtilities;
using Realta.Contract.Models;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.Features;
using System.Text;
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

        public async Task CreateProduct(RestoMenusDto restoMenusDto)
        {
            var content = JsonSerializer.Serialize(restoMenusDto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
           
            var postResult = await _httpClient.PostAsync("RestoMenus", bodyContent);
            var postContent = await postResult.Content.ReadAsStringAsync();


            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            } 
        }

        public async Task DeleteRestoMenus(int id)
        {
            var url = Path.Combine("RestoMenus", id.ToString());

            var deleteResult = await _httpClient.DeleteAsync(url);
            var deleteContent = await deleteResult.Content.ReadAsStringAsync();

            if (!deleteResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(deleteContent);
            }
        } 

        public async Task<PagingResponse<RestoMenusDto>> GetPaging(RestoMenusParameters restoMenuParameters)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = restoMenuParameters.PageNumber.ToString(),
                ["searchTerm"] = restoMenuParameters.SearchTerm == null ? "" : restoMenuParameters.SearchTerm,
                ["orderBy"] = restoMenuParameters.orderBy

            };

            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("RestoMenus/pageList", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }



            var pagingResponse = new PagingResponse<RestoMenusDto>
            {
                Items = JsonSerializer.Deserialize<List<RestoMenusDto>>(content, _options),
            MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(),_options)
            };

            return pagingResponse;
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

        public async Task UpdateRestomenus(RestoMenusDto restoMenusDto)
        {
            var content = JsonSerializer.Serialize(restoMenusDto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var url = Path.Combine("RestoMenus/", restoMenusDto.RemeId.ToString());

            var postResult = await _httpClient.PutAsync(url, bodyContent);
            var postContent = await postResult.Content.ReadAsStringAsync();

            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }
        }
    }
}
