using Microsoft.AspNetCore.Components;
using Realta.Contract.Models;
using Realta.Frontend.HttpRepository.Resto;

namespace Realta.Frontend.Pages.Resto
{
    public partial class RestoMenus
    {

        [Inject]
        public IRestoMenusHttpRepository RestoMenusRepo { get; set; }
        public List<RestoMenusDto> RestoMenusList { get; set; } = new List<RestoMenusDto>();

        protected async override Task OnInitializedAsync()
        {
            RestoMenusList = await RestoMenusRepo.GetRestoMenus();
          
        }

    }
}
