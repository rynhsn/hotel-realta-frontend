using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Realta.Contract.Models;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.HttpRepository.Booking;

namespace Realta.Frontend.Components.Booking
{
    public partial class HotelTable
    {
        [Parameter]
        public List<HotelsDto> HotelsList { get; set; } = new List<HotelsDto>();
        [Inject] 
        public IHotelHttpRepository HotelHttpRepository { get; set; }
        protected async override Task OnInitializedAsync()
        {
            HotelsList= await HotelHttpRepository.GetHotels();
            
            Console.WriteLine("HotelsList");
        }
        public List<SpecialOffersDto> SpecialOffersList { get; set; } = new List<SpecialOffersDto>();
        
        private HotelParameters _hotelParameters = new HotelParameters();
        public MetaData MetaData { get; set; } = new MetaData();
        private async Task SelectedPage(int page)
        {
            _hotelParameters.PageNumber = page;
            await GetHotelPaging();
        }
        private async Task GetHotelPaging()
        {
            var pagingResponse = await HotelHttpRepository.GetHotelPaging(_hotelParameters);
            HotelsList = pagingResponse.Items;
            MetaData = pagingResponse.MetaData;
        }
        private async Task SeacrhChange(string searchTerm)
        {
            Console.WriteLine(searchTerm);
            _hotelParameters.PageNumber = 0;
            _hotelParameters.SearchTerm = searchTerm;
            await GetHotelPaging();
        }
    }
}
