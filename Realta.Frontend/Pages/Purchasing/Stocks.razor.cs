using Microsoft.AspNetCore.Components;
using Realta.Contract.Models;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.HttpRepository.Purchasing;

namespace Realta.Frontend.Pages.Purchasing;

public partial class Stocks
{
    [Inject]
    public IStocksHttpRepository StocksHttpRepository { get; set; }
    public List<StocksDto> stocksList { get; set; } = new List<StocksDto>();
    public MetaData MetaData { get; set; }
    private StocksParameters _stocksParameters = new StocksParameters();

    protected async override Task OnInitializedAsync()
    {
        await GetPaging();
    }

    private async Task SelectedPage(int page)
    {
        _stocksParameters.PageNumber = page;
        await GetPaging();
    }
    private async Task GetPaging()
    {
        var response = await StocksHttpRepository.GetStocksPaging(_stocksParameters);
        stocksList = response.Items;
        MetaData = response.MetaData;
    }

    

}
