using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Realta.Contract.Models;

namespace Realta.Frontend.Components.Booking
{
    public partial class HotelTable
    {
        [Parameter]
        public List<HotelsDto> hotels { get; set; }
        
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IJSRuntime Js { get; set; }

        [Parameter]
        public EventCallback<int> OnDeleted { get; set; }
        
        private void RedirectToUpdate(int id)
        {
            var url = Path.Combine("/updateHotel/",id.ToString());
            NavigationManager.NavigateTo(url);
        }
        
        private async Task Delete(int id)
        {
            var product = hotels.FirstOrDefault(p => p.HotelId.Equals(id));
            var confirmed = await Js.InvokeAsync<bool>("confirm", $"Delete product {product.HotelName} ?");
            if (confirmed)
            {
                await OnDeleted.InvokeAsync(id);
            }
        }
    }
}
