using Realta.Domain.RequestFeatures;
using Realta.Frontend.Features;
using Realta.Contract.Models;

namespace Realta.Frontend.HttpRepository.Booking;


public interface IHotelHttpRepository
{
    Task<List<HotelsDto>?> GetHotels();

    Task<List<HotelsDto>> GetHotelsById(int id);
    
    Task<HotelsDto> GetHotelsByIdSingle(int id);

    Task<PagingResponse<HotelsDto>> GetHotelPaging(HotelParameters hotelParameters);

    
}