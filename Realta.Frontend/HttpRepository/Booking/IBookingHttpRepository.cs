 using Realta.Domain.RequestFeatures;
using Realta.Frontend.Features;
using Realta.Contract.Models;
namespace Realta.Frontend.HttpRepository.Booking;

public interface IBookingHttpRepository
{
    
    
    Task<BookingOrdersDto> GetBookingOrdersById (int id);
    Task<List<BookingOrderDetailDto>> GetBookingOrderDetailByBoorId(int id);
    Task<List<BookingOrderDetailExtraDto>> GetBookingOrderDetailExtraByBoorId(int id);
    Task<UserMemberDto> GetUserMemberByBoorId(int id);
    Task<List<PriceItemsDto>> GetPriceItems();
    Task<UserDto> GetUserbyId(int id);

}