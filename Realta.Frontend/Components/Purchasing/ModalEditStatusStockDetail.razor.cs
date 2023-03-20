using Microsoft.AspNetCore.Components;
using Realta.Contract.Models;
using Realta.Frontend.HttpRepository.Purchasing;
using Realta.Frontend.Shared;

namespace Realta.Frontend.Components.Purchasing;

public partial class ModalEditStatusStockDetail
{
    private string _modalClass;
    private string _modalDisplay;
    private bool _showBackdrop;

    private SuccessNotification _notification;
    [Parameter] public int? StockId { get; set; }

    public async Task Show()
    {
        _modalClass = "show";
        _modalDisplay = "block;";
        _showBackdrop = true;

        StateHasChanged();
    }
  
    public void Hide()
    {
        _modalClass = "";
        _modalDisplay = "none;";
        _showBackdrop = false;

        StateHasChanged();
    }
    [Inject] public IStockDetailHttpRepository StockDetailHttpRepository { get; set; }
    [Parameter] public EventCallback getPaging { get; set; }
    [Parameter] public StockDetailDto stocksDetail { get; set; } = new StockDetailDto();
    private async Task Update()
    {
        await StockDetailHttpRepository.EditStatus(stocksDetail);
        _notification.Show($"/purchasing/stock/{StockId}", "Data has been updated.");
        await getPaging.InvokeAsync();
    }
}