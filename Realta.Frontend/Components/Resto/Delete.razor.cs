//using Microsoft.AspNetCore.Components;
//using Microsoft.JSInterop;
//using Realta.Contract.Models;
//using Realta.Frontend.Pages.Purchasing;

//namespace Realta.Frontend.Components.Resto
//{
//    public partial class Delete
//    {
//        [Parameter]
//        public List<RestoMenusDto> RestoMenusList { get; set; }

//        [Inject]
//        public NavigationManager NavigationManager { get; set; }

//        [Inject]
//        public IJSRuntime Js { get; set; }

//        [Parameter]
//        public EventCallback<int> OnDeleted { get; set; }

//        private async Task Delete(int id)
//        {
//            var restoMenus = RestoMenusList.FirstOrDefault(p => p.RemeId.Equals(id));
//            var confirmed = await Js.InvokeAsync<bool>("confirm", $"Delete product {restoMenus.RemeName} ?");
//            if (confirmed)
//            {
//                await OnDeleted.InvokeAsync(id);
//            }
//        }
//    }
//}   
