using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Realta.Contract.Models;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.Components;
using Realta.Frontend.Components.Purchasing;
using Realta.Frontend.HttpRepository.Purchasing;
using Realta.Frontend.Pages.Resto;
using Realta.Frontend.Shared;

namespace Realta.Frontend.Pages.Purchasing;

public partial class Stocks
{
    [Inject]
    public IStocksHttpRepository StocksHttpRepository { get; set; }

    public List<StocksDto> stocksList { get; set; } = new List<StocksDto>();

    public MetaData MetaData { get; set; } = new MetaData();
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

    private async Task SearchChange(string searchTerm)
    {
        _stocksParameters.PageNumber = 1;
        _stocksParameters.SearchTerm = searchTerm;
        await GetPaging();
    }

    private async Task applySort(ChangeEventArgs eventArgs)
    {
        _stocksParameters.OrderBy = eventArgs.Value.ToString();
        await GetPaging();
    }

    private ModalCreateStock _createStock;

    private ModalUpdateStock _updateStock;

    public StocksDto GetUpdateStock { get; set; }
    private async Task UpdateStock(int id)
    {
        GetUpdateStock = await StocksHttpRepository.GetStockById(id);
        Task.Delay(100);
        await _updateStock.Show();
    }

    private ModalDelete _del;

    private async Task OnDelete(int id)
    {
        var stocks = stocksList.FirstOrDefault(s => s.StockId.Equals(id));
        _del.Show<int>(id, $"Stock {stocks.StockName} will be deleted!");
        await Task.Delay(100);
    }
    private async Task OnDeleteConfirmed(object id)
    {
        _del.Hide();
        await StocksHttpRepository.DeleteStock((int)id);
        _stocksParameters.PageNumber = 1;
        await GetPaging();
    }

    private ModalUploadFotoStock _uploadFoto;

    private async Task UploadFoto(int? id)
    {
        var photoList = await StocksHttpRepository.GetStocksPhoto(id.Value);
        Task.Delay(100);
        await _uploadFoto.Show(photoList);
        
    }

}
