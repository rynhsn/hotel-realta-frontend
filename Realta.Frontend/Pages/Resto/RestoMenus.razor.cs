using Microsoft.AspNetCore.Components;
using Realta.Contract.Models;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.HttpRepository.Resto;
using Microsoft.AspNetCore.Hosting;
using Realta.Frontend.Components.Resto;
using Microsoft.JSInterop;

namespace Realta.Frontend.Pages.Resto
{
    public partial class RestoMenus
    {

        [Inject]
        public IRestoMenusHttpRepository RestoMenusRepo { get; set; } //
        public List<RestoMenusDto> RestoMenusList { get; set; } = new List<RestoMenusDto>(); //
        private RestoMenusDto _restoDto = new RestoMenusDto();

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
            _restoMenusParameters.PageSize = 5;
            var pagingResponse = await RestoMenusRepo.GetPaging(_restoMenusParameters);
            RestoMenusListPaging = pagingResponse.Items;
            MetaData = pagingResponse.MetaData;
        }
        private async Task SelectedPage(int page)
        {
            _restoMenusParameters.PageNumber = page;
            await GetRestoPaging();
        }

        private async Task SearchChange(string searchTerm)
        {

            Console.WriteLine(searchTerm);

            _restoMenusParameters.PageNumber = 1;
            _restoMenusParameters.SearchTerm = searchTerm;

            await GetRestoPaging();

        }

        private async Task SortChgane(string orderBy)

        {
            _restoMenusParameters.orderBy = orderBy;

            await GetRestoPaging();

        }

        private RestoMenusDto _restoMenus = new RestoMenusDto();


        private SuccessNotification _notification;



        private async Task Create()
        {
            await RestoMenusRepo.CreateProduct(_restoMenus);
            _notification.Show("RestoMenus");
            await GetRestoPaging();
        }

        [Inject]
        public IJSRuntime Js { get; set; }


        private async Task Delete(int id)
        {
            var product = RestoMenusListPaging.FirstOrDefault(p => p.RemeId.Equals(id));
            var confirmed = await Js.InvokeAsync<bool>("confirm", $"Delete product {product.RemeName} ?");
            if (confirmed)
            {
                await RestoMenusRepo.DeleteRestoMenus(id);
                _restoMenusParameters.PageNumber = 1;
                await GetRestoPaging();
            }
        }

        private async Task OnUpdate(RestoMenusDto Data)
        {
            _restoDto.RemeId = Data.RemeId;
            _restoDto.RemeName = Data.RemeName;
            _restoDto.RemePrice = Data.RemePrice;
            _restoDto.RemeType = Data.RemeType;
            _restoDto.RemeDescription = Data.RemeDescription;
            _restoDto.RemeStatus = Data.RemeStatus;
        }
        private async Task OnUpdateConf()
        {
            await RestoMenusRepo.UpdateRestomenus(_restoDto);
            _restoMenusParameters.PageNumber = 1;
            await GetRestoPaging();
            _notification.Show("/restoMenus");
        }



    }
}