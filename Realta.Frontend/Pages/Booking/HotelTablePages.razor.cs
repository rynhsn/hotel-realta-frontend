using Realta.Contract.Models;
using Realta.Frontend.HttpRepository.Booking;
using Microsoft.AspNetCore.Components;
using Realta.Domain.RequestFeatures;

namespace Realta.Frontend.Pages.Booking
{
    public partial class HotelTablePages
    {

        private async Task SeacrhChange(string searchTerm)
        {
            Console.WriteLine(searchTerm);
            _hotelParameters.PageNumber = 0;
            _hotelParameters.SearchTerm = searchTerm;
            await GetHotelPaging();
        }
        [Parameter]
        public List<HotelsDto> HotelsList { get; set; } = new List<HotelsDto>();
        public MetaData MetaData { get; set; } = new MetaData();
        [Inject] public IHotelHttpRepository HotelHttpRepository { get; set; }

        protected async override Task OnInitializedAsync()
        {
            HotelsList = await HotelHttpRepository.GetHotels();

            Console.WriteLine("HotelsList");
        }
        
        private HotelParameters _hotelParameters = new HotelParameters();
        
        private async Task GetHotelPaging()
        {
            var pagingResponse = await HotelHttpRepository.GetHotelPaging(_hotelParameters);
            HotelsList = pagingResponse.Items;
            MetaData = pagingResponse.MetaData;
        }
        
        private async Task SelectedPage(int page)
        {
            _hotelParameters.PageNumber = page;
            await GetHotelPaging();
        }
        

        
    }
}
