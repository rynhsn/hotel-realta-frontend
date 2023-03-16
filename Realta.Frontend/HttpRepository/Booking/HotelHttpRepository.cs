using Realta.Frontend.Components;
using System.Text.Json;
using Microsoft.AspNetCore.WebUtilities;
using Realta.Contract.Models;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.Features;

namespace Realta.Frontend.HttpRepository.Booking;

public class HotelHttpRepository : IHotelHttpRepository
{
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _options;

    public HotelHttpRepository(HttpClient client)
    {
        _client = client;
        _options = new JsonSerializerOptions{ PropertyNameCaseInsensitive = true};
    }


    public async Task<List<HotelsDto>?> GetHotels()
    {
        //Call API endpoint, e.g : https://localhost:7068/api/BookingOrders/hotelList
        var response = await _client.GetAsync("BookingOrders/hotelList");
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException(content);
        }
        
        var result  = JsonSerializer.Deserialize<List<HotelsDto>>(content, _options);
        return result;
    }

    public async Task<List<HotelsDto>> GetHotelsById(int id)
    {
        var url = Path.Combine("Booking", id.ToString());

        var response = await _client.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }

        var hotel = JsonSerializer.Deserialize<List<HotelsDto>>(content, _options);
        return hotel;
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