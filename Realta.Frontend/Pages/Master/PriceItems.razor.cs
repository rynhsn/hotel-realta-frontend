using Microsoft.AspNetCore.Components;
using Realta.Contract.Models;
using Realta.Frontend.HttpRepository.Master.PriceItems;

namespace Realta.Frontend.Pages.Master
{
    public partial class PriceItems
    {
        [Inject]

        public IPriceItemsHttpRepository PriceItemsRepository { get; set;}
        public  List<PriceItemsDto> PriceItemsList { get; set;} = new List<PriceItemsDto>();

        protected async override Task OnInitializedAsync()
        {
            PriceItemsList = await PriceItemsRepository.GetPriceItems();
        }
    }
}
