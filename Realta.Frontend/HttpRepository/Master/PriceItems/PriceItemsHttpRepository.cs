using Realta.Contract.Models;
using System.Text.Json;
using Microsoft.AspNetCore.WebUtilities;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.Features;
using System.Text;

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
                ["pageNumber"] = priceItemsParameters.PageNumber.ToString(),
                ["searchTerm"] = priceItemsParameters.SearchTerm == null ? "" : priceItemsParameters.SearchTerm,
                ["orderBy"] = priceItemsParameters.OrderBy
            };
            var response =
                await _httpClient.GetAsync(QueryHelpers.AddQueryString("priceitems/pageList", queryStringParam));
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
            Console.WriteLine(pagingRespone.Items);
            return pagingRespone;
        }

        public async Task CreatePriceItems(PriceItemsCreateDto priceItemsCreateDto)
        {
            var content = JsonSerializer.Serialize(priceItemsCreateDto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var postResult = await _httpClient.PostAsync("priceitems", bodyContent);
            var postContent = await postResult.Content.ReadAsStringAsync();

            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }
        }

        public async Task UpdatePriceItems(PriceItemsDto priceItemsDto)
        {
            var content = JsonSerializer.Serialize(priceItemsDto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var url = Path.Combine("priceitems", priceItemsDto.PritId.ToString());

            var postResult = await _httpClient.PutAsync(url, bodyContent);
            var postContent = await postResult.Content.ReadAsStringAsync();


            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }

        }

        public async Task<PriceItemsDto> GetPriceItemsById(int id)
        {
            var url = Path.Combine("priceitems", id.ToString());

            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var priceItemsDto = JsonSerializer.Deserialize<PriceItemsDto>(content, _options);
            return priceItemsDto;
        }

        public async Task deletePriceItems(int id)
        {
            var url = Path.Combine("priceitems", id.ToString());

            var deleteResult = await _httpClient.DeleteAsync(url);
            var deleteContent = await deleteResult.Content.ReadAsStringAsync();

            if (!deleteResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(deleteContent);
            }
        }
    }
}