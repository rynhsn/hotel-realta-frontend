using Microsoft.AspNetCore.WebUtilities;
using Realta.Contract.Models;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.Features;
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

        public async Task<PagingResponse<RestoMenusDto>> GetPaging(RestoMenusParameters restoMenuParameters)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = restoMenuParameters.PageNumber.ToString(),
                ["searchTerm"] = restoMenuParameters.SearchTerm == null ? "" : restoMenuParameters.SearchTerm

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
    }
}
