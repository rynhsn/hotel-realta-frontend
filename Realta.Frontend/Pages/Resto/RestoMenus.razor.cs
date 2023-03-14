using Microsoft.AspNetCore.Components;
using Realta.Contract.Models;
using Realta.Domain.RequestFeatures;
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
            //RestoMenusList = await RestoMenusRepo.GetRestoMenus();
            await GetRestoPaging();
        }

        private RestoMenusParameters _restoMenusParameters = new RestoMenusParameters();
        public List<RestoMenusDto> RestoMenusListPaging { get; set; } = new List<RestoMenusDto>();

        public MetaData MetaData { get; set; } = new MetaData();

        private async Task GetRestoPaging()
        {
            var pagingResponse = await RestoMenusRepo.GetPaging(_restoMenusParameters);
            RestoMenusListPaging = pagingResponse.Items;
            MetaData = pagingResponse.MetaData;
        }
        private async Task SelectedPage(int page)
        {
            _restoMenusParameters.PageNumber = page;
            await GetRestoPaging();
        }

        private async Task SearchChange(string searchTerm) {

            Console.WriteLine(searchTerm);

            _restoMenusParameters.PageNumber = 1;
            _restoMenusParameters.SearchTerm= searchTerm;

            await GetRestoPaging();
        
        }


    }
}
