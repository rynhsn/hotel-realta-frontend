using Realta.Contract.Models;
using System.Text.Json;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.Features;
using Microsoft.AspNetCore.WebUtilities;

namespace Realta.Frontend.HttpRepository.Master
{
    public class CountryHttpRepository : ICountryHttpRepository
    {

        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;

        public CountryHttpRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<List<CountryDto>> GetCountry()
        {

            //AsyncCallback api end point e.g https://localhost:7068/api/country
            var response = await _httpClient.GetAsync("country");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var country = JsonSerializer.Deserialize<List<CountryDto>>(content, _options);
            return country;
        }

        public async Task<PagingResponse<CountryDto>> GetCountryPaging(CountryParameters countryParameters)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = countryParameters.PageNumber.ToString(),
                ["searchTerm"] = countryParameters.SearchTerm == null ? "" : countryParameters.SearchTerm
            };
            var response =
                await _httpClient.GetAsync(QueryHelpers.AddQueryString("country/pageList", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var pagingRespone = new PagingResponse<CountryDto>
            {
                Items = JsonSerializer.Deserialize<List<CountryDto>>(content, _options),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(),
                    _options)
            };
            Console.WriteLine(pagingRespone.Items);
            return pagingRespone;

        }
    }
}
