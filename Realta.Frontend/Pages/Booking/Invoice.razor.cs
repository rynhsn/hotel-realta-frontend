using System.Collections;
using Realta.Contract.Models;
using Realta.Frontend.HttpRepository.Booking;
using Microsoft.AspNetCore.Components;
using Realta.Domain.Entities;
using Realta.Domain.RequestFeatures;

namespace Realta.Frontend.Pages.Booking;

public partial class Invoice
{
    [Parameter]
    public int Id { get; set; }
    public BookingOrdersDto? Bookings { get; set; } = new BookingOrdersDto();
    public BookingOrderDetailDto? BookingOrderDetail { get; set; } = new BookingOrderDetailDto();
    public BookingOrderDetailExtraDto? BookingOrderDetailExtra { get; set; } = new BookingOrderDetailExtraDto();
    public UserMemberDto UserInfo { get; set; } = new UserMemberDto();
  
    //variable untuk menampung data dari endpoint
    public List<BookingOrderDetailDto> BookingDetailByBoorId { get; set; } = new List<BookingOrderDetailDto>();
    public List<BookingOrderDetailExtraDto> BookingExtraByBoorId { get; set; } = new List<BookingOrderDetailExtraDto>();
    
    [Inject]
    public IBookingHttpRepository BookingHttpRepository {get; set; }
  
    protected async override Task OnInitializedAsync()
    {
        Bookings = await BookingHttpRepository.GetBookingOrdersById(Id);
        BookingDetailByBoorId = await BookingHttpRepository.GetBookingOrderDetailByBoorId(Id);
        BookingExtraByBoorId = await BookingHttpRepository.GetBookingOrderDetailExtraByBoorId(Id);
        UserInfo = await BookingHttpRepository.GetUserMemberByBoorId(Id);    
    }
}