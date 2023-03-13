using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Realta.Contract.Models;

namespace Realta.Frontend.Components.Booking
{
    public partial class SpofTable
    {   
        [Parameter] public List<SpecialOffersDto> SpecialOffer { get; set; }

    }
}