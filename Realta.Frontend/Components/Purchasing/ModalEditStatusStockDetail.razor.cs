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
    [Parameter] public UpdateStatusStockDetailDto stocksDetail { get; set; } 
    private SuccessNotification _notification;

    private async Task Update()
    {
        await StockDetailHttpRepository.EditStatus(stocksDetail);
        _notification.Show($"/purchasing/stock/{StockId}", "Status Has been update");
        await getPaging.InvokeAsync();
    }

    //  SearchWithPopupData Faci
    private Timer _timer;
    [Parameter] public List<StocksDto> DataPopup { get; set; }
    [Parameter] public Task<List<StocksDto>> MyParameterAsync { get; set; }
    private string _styleDiplay;
    [Parameter] public EventCallback<string> OnSearchChanged { get; set; }

    private void SearchChanged(ChangeEventArgs e)
    {
        if (_timer != null)
            _timer.Dispose();
        _timer = new Timer(OnTimerElapsed, e.Value, 500, 0);
        if (DataPopup.Count > 0 && e.Value.ToString() != "")
        {
            _styleDiplay = "block";
        }
        else
        {
            _styleDiplay = "none";
        }
    }

    private void OnTimerElapsed(object? sender)
    {
        OnSearchChanged.InvokeAsync(sender.ToString());
        _timer.Dispose();
    }

    private string? searchFaci;
    private void selectedRoom(int faciId, string selected)
    {
        searchFaci = selected;
        _styleDiplay = "none";
    }
}