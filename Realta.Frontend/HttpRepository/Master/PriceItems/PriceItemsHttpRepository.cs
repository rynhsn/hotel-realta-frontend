using Realta.Contract.Models;
using System.Text.Json;
using Microsoft.AspNetCore.WebUtilities;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.Features;

namespace Realta.Frontend.HttpRepository.Master.PriceItems
{
    public class PriceItemsHttpRepository : IPriceItemsHttpRepository
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;

        public PriceItemsHttpRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<List<PriceItemsDto>> GetPriceItems()
        {
            //AsyncCallback api end point e.g https://localhost:7068/api/priceitems  
            var response = await _httpClient.GetAsync("priceitems");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var prit = JsonSerializer.Deserialize<List<PriceItemsDto>>(content, _options);
            return prit;
        }

        public async Task<PagingResponse<PriceItemsDto>> GetPriceItemsPaging(PriceItemsParameters priceItemsParameters)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = priceItemsParameters.PageNumber.ToString()
            };
            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("priceitems/pageList", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var pagingRespone = new PagingResponse<PriceItemsDto>
            {
                Items = JsonSerializer.Deserialize<List<PriceItemsDto>>(content, _options),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(),
                    _options)
            };
            return pagingRespone;
        }
    }
}