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
    //
    // [Parameter]
    // public BookingOrdersDto BookingOrderList { get; set; } 
    //
    // [Parameter]
    // public List<BookingOrderDetailDto> BookingOrderDetailList { get; set; }
    //
    // [Parameter]
    // public List<BookingOrderDetailExtraDto> BookingOrderDetailExtraList { get; set; }
}