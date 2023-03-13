using Realta.Contract.Models;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.HttpRepository.Booking;

namespace Realta.Frontend.Pages.Booking
{
    public partial class HotelPaging
    {
        public List<HotelsDto> HotelListPaging { get; set; } = new List<HotelsDto>();
        
        public MetaData MetaData { get; set; } = new MetaData();
        
        private HotelParameters _hotelParameters = new HotelParameters();

        private IHotelHttpRepository HotelRepository { get; set; }
        
        
        private async Task GetHotelPaging()
        {
            var pagingResponse = await HotelRepository.GetHotelPaging(_hotelParameters);
            HotelListPaging = pagingResponse.Items;
            MetaData = pagingResponse.MetaData;
        }
        private async Task SelectedPage(int page)
        {
            _hotelParameters.PageNumber = page;
            // await GetHotelPaging();
            
        }
        
        protected async override Task OnInitializedAsync()
        {
            await GetHotelPaging();
        }


    }
}
