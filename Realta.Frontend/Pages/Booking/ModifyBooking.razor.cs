using System.Collections;
using Realta.Contract.Models;
using Realta.Frontend.HttpRepository.Booking;
using Microsoft.AspNetCore.Components;
using Realta.Domain.RequestFeatures;
namespace Realta.Frontend.Pages.Booking;

public partial class ModifyBooking
{
    [Parameter]
    public int Id { get; set; }

    [Inject] 
    public ISpecialOfferHttpRepository SpecialOfferHttpRepository { get; set; }

    [Inject]
    public IBookingHttpRepository BookingHttpRepository { get; set; }
    
    [Inject]
    public IHotelHttpRepository HotelHttpRepository { get; set; }
    
    
    public HotelsDto Room { get; set; } = new HotelsDto();

    public List<SpecialOffersDto> SpecialOffersList { get; set; } = new List<SpecialOffersDto>();

    public List<PriceItemsDto> PriceItemsList { get; set; } = new List<PriceItemsDto>();
    
    public UserDto UserById { get; set; } = new UserDto();
    
    protected async override Task OnInitializedAsync()
    {

        Room = await HotelHttpRepository.GetHotelsByIdSingle(Id);
        SpecialOffersList= await SpecialOfferHttpRepository.GetSpecialOffers();
        PriceItemsList = await BookingHttpRepository.GetPriceItems();
        UserById = await BookingHttpRepository.GetUserbyId(1);
    }
}