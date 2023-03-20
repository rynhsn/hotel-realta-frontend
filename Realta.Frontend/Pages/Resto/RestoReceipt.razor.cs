using Microsoft.AspNetCore.Components;
using Realta.Contract.Models;
using Realta.Frontend.HttpRepository.Resto;

namespace Realta.Frontend.Pages.Resto
{
    public partial class RestoReceipt
    {
        private int userid = 1;
        [Inject]
        public IOrmeDetailHttpRepository OrmeRepo { get; set; }
        public List<OrderMenusDto> OrmeList { get; set; } = new List<OrderMenusDto>();
        private OrderMenusDto _toGet = new();
        protected async override Task OnInitializedAsync()
        {
            OrmeList = await OrmeRepo.GetOrderMenus();

            OrmeList = await OrmeRepo.GetOrderMenus();
            _toGet = OrmeList.LastOrDefault(r => r.OrmeUserId == userid && r.OrmeStatus == "Ordered");
            Console.WriteLine(_toGet.OrmeCardnumber);
        }

       
        }
}
