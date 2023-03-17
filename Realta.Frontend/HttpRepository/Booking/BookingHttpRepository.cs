using Realta.Frontend.Components;
using System.Text.Json;
using Microsoft.AspNetCore.WebUtilities;
using Realta.Contract.Models;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.Features;

namespace Realta.Frontend.HttpRepository.Booking;

public class BookingHttpRepository : IBookingHttpRepository
{
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _options;

    public BookingHttpRepository(HttpClient client)
    {
        _client = client;
        _options = new JsonSerializerOptions{ PropertyNameCaseInsensitive = true};
    }
    

    public async Task<BookingOrdersDto> GetBookingOrdersById(int id)
    {
        var url = Path.Combine("BookingOrders", id.ToString());

        var response = await _client.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
        
        var boor = JsonSerializer.Deserialize<BookingOrdersDto>(content, _options);
        return boor;
    }
    
    public async Task<List<BookingOrderDetailDto>> GetBookingOrderDetailByBoorId(int id)
    {
        var url = Path.Combine("BookingOrderDetail/boor", id.ToString());

        var response = await _client.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
        
        var borde = JsonSerializer.Deserialize<List<BookingOrderDetailDto>>(content, _options);
        return borde;
    }

    public async Task<List<BookingOrderDetailExtraDto>> GetBookingOrderDetailExtraByBoorId(int id)
    {
        var url = Path.Combine("BookingOrderDetailExtra/boor", id.ToString());

        var response = await _client.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
        
        var boex = JsonSerializer.Deserialize<List<BookingOrderDetailExtraDto>>(content, _options);
        return boex;
    }

    public async Task<UserMemberDto> GetUserMemberByBoorId(int id)
    {
        var url = Path.Combine("BookingOrders/user", id.ToString());

        var response = await _client.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
        
        var usme = JsonSerializer.Deserialize<UserMemberDto>(content, _options);
        return usme;
    }
}