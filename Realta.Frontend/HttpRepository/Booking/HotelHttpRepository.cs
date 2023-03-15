using Realta.Domain.RequestFeatures;
using Realta.Frontend.Features;

namespace Realta.Frontend.HttpRepository.Booking;
using Realta.Contract.Models;
using System.Text.Json;
using Microsoft.AspNetCore.WebUtilities;


public class HotelHttpRepository : IHotelHttpRepository
{
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _options;

    public HotelHttpRepository(HttpClient client, JsonSerializerOptions options)
    {
        _client = client;
        _options = new JsonSerializerOptions{ PropertyNameCaseInsensitive = true};
    }


    public async Task<List<HotelsDto>?> GetHotels()
    {
        var response = await _client.GetAsync("BookingOrders/hotelList");
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException(content);
        }
        
        var result  = JsonSerializer.Deserialize<List<HotelsDto>>(content, _options);
        return result;
    }
    public async Task<PagingResponse<HotelsDto>> GetHotelPaging(HotelParameters hotelParameters)
    {
        var queryStringParam = new Dictionary<string, string>
        {
            ["PageNumber"] = hotelParameters.PageNumber.ToString(),
        };
        
        var response = await _client.GetAsync(QueryHelpers.AddQueryString("BookingOrders/hotelList",queryStringParam));
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException(content);
        }

        var pagingResponse = new PagingResponse<HotelsDto>
        {
            Items = JsonSerializer.Deserialize<List<HotelsDto>>(content, _options),
            MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(),
                _options)
        };

        return pagingResponse;
    }
}