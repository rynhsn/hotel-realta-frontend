using Realta.Frontend.Components;
using System.Text.Json;
using Microsoft.AspNetCore.WebUtilities;
using Realta.Contract.Models;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.Features;

namespace Realta.Frontend.HttpRepository.Booking
{
    public class SpecialOfferHttpRepository : ISpecialOfferHttpRepository
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;
        
        public SpecialOfferHttpRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }
        
        public async Task<List<SpecialOffersDto>> GetSpecialOffers()
        {
            //Call API endpoint, e.g : http://localhost:7068/api/SpecialOffers
            var response = await _httpClient.GetAsync("SpecialOffers");
            var content = await response.Content.ReadAsStringAsync();
            
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var specialOffers = JsonSerializer.Deserialize<List<SpecialOffersDto>>(content, _options);
            return specialOffers;
        }

        public async Task<PagingResponse<SpecialOffersDto>> GetSpecialOfferPaging(SpecialOfferParameters specialOfferParameters)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = specialOfferParameters.PageNumber.ToString(),
                ["searchTerm"] = specialOfferParameters.SearchTerm == null ? "" : specialOfferParameters.SearchTerm,
                ["orderBy"] = specialOfferParameters.OrderBy

            };
            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("SpecialOffers/pageList", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();
            
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var pagingResponse = new PagingResponse<SpecialOffersDto>
            {
                Items = JsonSerializer.Deserialize<List<SpecialOffersDto>>(content, _options),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(),
                    _options)
            };
            return pagingResponse;
        }
        
    }
}
