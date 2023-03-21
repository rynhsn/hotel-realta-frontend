using Microsoft.AspNetCore.Components;
using Realta.Contract.Models;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.Components.Purchasing;
using Realta.Frontend.HttpRepository.Purchasing;
using System.ComponentModel;

namespace Realta.Frontend.Pages.Purchasing
{
    public partial class StockDetail
    {
        [Parameter] public int? Id { get; set; }

        [Inject] public IStockDetailHttpRepository StockDetailHttpRepository { get; set;}
        public MetaData MetaData { get; set; } = new MetaData();
        public List<StockDetailDto> stocksDetailList { get; set; }

        public StockDetailParameters _stockDetailParameters = new StockDetailParameters();

        protected async override Task OnInitializedAsync()
        {
            await GetPaging();
            await GetFaci();
        }

        private async Task SelectedPage(int page)
        {
            _stockDetailParameters.PageNumber = page;
            await GetPaging();
        }
        private async Task GetPaging()
        {
            if (Id != null)
            {
                _stockDetailParameters.StockId = Id.Value;
                var response = await StockDetailHttpRepository.GetStockDetailPaging(_stockDetailParameters);
                stocksDetailList = response.Items;
                MetaData = response.MetaData;
            }
        }

        public static (string, string) GetStatus(int status)
        {
            return status switch
            {
                1 => ("warning-btn", "Stocked"),
                2 => ("info-btn", "Used"),
                3 => ("danger-btn", "Broken"),
                4 => ("dark-btn", "Complete")
            };
        }

        private ModalEditStatusStockDetail _editStatus; 
        public UpdateStatusStockDetailDto EditStatusDetail { get; set; } = new UpdateStatusStockDetailDto();
        private async Task EditStatus(int id)
        {
            var getStockDetailDto = await StockDetailHttpRepository.GetStockDetailById(id);
            EditStatusDetail.StodId = getStockDetailDto.StodId;
            EditStatusDetail.StodStatus = getStockDetailDto.StodStatus;
            EditStatusDetail.StodNotes = getStockDetailDto.StodNotes;
            EditStatusDetail.StodFaciId = getStockDetailDto.StodFaciId;
            Task.Delay(100);
            await _editStatus.Show();
        }

        [Inject] public IStocksHttpRepository StocksHttpRepository { get; set; }
        public List<StocksDto> stocksList { get; set; } = new List<StocksDto>();
        private StocksParameters _stocksParameters = new StocksParameters();
        private async Task GetFaci()
        {
             var response = await StocksHttpRepository.GetStocksPaging(_stocksParameters);
             stocksList =  response.Items;

        }
        private async Task SearchChangeFaci(string searchTerm)
        {
            _stocksParameters.SearchTerm = searchTerm;
            await GetFaci();
        }
    }

}
