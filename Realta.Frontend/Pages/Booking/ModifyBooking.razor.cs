using System.Collections;
using Realta.Contract.Models;
using Realta.Frontend.HttpRepository.Booking;
using Microsoft.AspNetCore.Components;
using Realta.Domain.RequestFeatures;
namespace Realta.Frontend.Pages.Booking;

public partial class ModifyBooking
{
    public class SpofItem
    {
        public List<SpecialOffersDto> SpecialOffersList { get; set; } = new List<SpecialOffersDto>();
        [Inject] 
        public ISpecialOfferHttpRepository SpecialOfferHttpRepository { get; set; }
        protected async  Task OnInitializedAsync()
        {
            SpecialOffersList= await SpecialOfferHttpRepository.GetSpecialOffers();
        }
    
    }
}