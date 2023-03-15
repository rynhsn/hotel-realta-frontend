using System.Collections;
using Realta.Contract.Models;
using Realta.Frontend.HttpRepository.Booking;
using Microsoft.AspNetCore.Components;
using Realta.Domain.RequestFeatures;

namespace Realta.Frontend.Pages.Booking;

public class HotelItem
{
    public List<HotelsDto> HotelsList { get; set; } = new List<HotelsDto>();
    [Inject] 
    public IHotelHttpRepository HotelHttpRepository { get; set; }
    
    protected async  Task OnInitializedAsync()
    {
        HotelsList= await HotelHttpRepository.GetHotels();
    }
    
    public MetaData MetaData { get; set; } = new MetaData();

}