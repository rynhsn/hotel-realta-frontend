using System.Collections;
using Realta.Contract.Models;
using Realta.Frontend.HttpRepository.Booking;
using Microsoft.AspNetCore.Components;
using Realta.Domain.RequestFeatures;

namespace Realta.Frontend.Pages.Booking;

public partial class HotelList
{
    [Parameter]
    public List<HotelsDto> HotelsList { get; set; }
    
    [Inject]
    public NavigationManager NavigationManager { get; set; }

    private void RedirectId(int id)
    {
        var pathUrl = Path.Combine("/booking/", id.ToString());
        NavigationManager.NavigateTo(pathUrl);
    }


}