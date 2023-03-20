using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Realta.Contract.Models;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.Components;
using Realta.Frontend.HttpRepository.Purchasing;
using Realta.Frontend.Shared;

namespace Realta.Frontend.Pages.Purchasing;

public partial class ListOrderDetail
{
    [Parameter] public string Id { get; set; }
    [Inject] private IJSRuntime Js { get; set; }
    [Inject] private IPurchaseOrderHttpRepository Repo { get; set; }    
    [Inject] private IStockDetailHttpRepository StockDetailHttpRepository { get; set; }
    
    private PurchaseOrderDto Header { get; set; } = new();
    private MetaData MetaData { get; set; } = new();
    private List<PurchaseOrderDetailDto> DataList { get; set; } = new();
    
    private PurchaseOrderDetailParameters _param = new();
    private QtyUpdateDto toUpdate = new();
    private SuccessNotification _notif;
    private ErrorNotifModal _error;
    private ModalDelete _del;
    private string orderBy = ""; // menunjukkan kolom yang diurutkan
    private string sortOrder = "asc"; // menunjukkan urutan sortir (asc atau desc)

    protected async override Task OnInitializedAsync()
    {
        await Get();
    }

    private async Task SelectedPage(int page)
    {
        _param.PageNumber = page;
        await Get();
    }

    private async Task Get()
    {
        Header = await Repo.GetHeader(Id);
        var response = await Repo.GetDetails(Id, _param);
        DataList = response.Items;
        MetaData = response.MetaData;
    }
    
    private async Task OnUpdate(PurchaseOrderDetailDto data)
    {
        toUpdate.PodeId = data.PodeId;
        toUpdate.StockName = data.StockName;
        toUpdate.PodeOrderQty = data.PodeOrderQty;
        toUpdate.PodeReceivedQty = data.PodeReceivedQty;
        toUpdate.PodeRejectedQty = data.PodeRejectedQty;
    }
    
    private async Task OnUpdateConfirmed()
    {
        if (toUpdate.PodeRejectedQty == 0 && toUpdate.PodeReceivedQty == 0) await OnUpdateStatus(2);
        if (toUpdate.PodeReceivedQty > 0 || toUpdate.PodeRejectedQty > 0 ) await OnUpdateStatus(4);
        await Task.Delay(100);
        await Repo.UpdateQty(toUpdate);
        _param.PageNumber = 1;
        await Get();
        var uri = Header != null ? NavigationManager.Uri : "/purchasing/list-order";
        _notif.Show(uri, "Data has been updated.");
    }

    private async Task OnUpdateStatus(byte status)
    {
        var header = new StatusUpdateDto()
        {
            PoheNumber = Header.PoheNumber,
            PoheStatus = status //rejected
        };
        await Repo.UpdateStatus(header);
    }

    private async Task OnDelete(int id)
    {
        _del.Show<int>(id, $"Purchase Order {id} will be deleted!");
        await Task.Delay(100);
    }

    private async Task OnDeleteConfirmed(object id)
    {
        _del.Hide();
        await Repo.DeleteDetail((int)id);
        await Task.Delay(100);
        await Get();
        var uri = Header != null ? NavigationManager.Uri : "/purchasing/list-order";
        _notif.Show(uri, "Data has been updated.");
    }

    private async Task GenerateBarcode(QtyUpdateDto PoUpdate)
    {
        if (Header.PoheStatus == 4)
        {
            // await OnUpdateStatus(5);
            await StockDetailHttpRepository.GenerateBarcode(PoUpdate);
            await Task.Delay(100);
            _notif.ShowWithoutPath();
        }
        else
        {
            _error.Show("Can't generate barcode, status must be recivied");
        }
    }

    private async Task SortChanged(string columnName)
    {
        if (orderBy != columnName)
        {
            // kolom baru yang di klik, urutan sortir diatur ulang ke ascending
            orderBy = columnName;
            sortOrder = "asc";
        }
        else
        {
            // kolom yang sama yang di klik, urutan sortir diubah antara ascending dan descending
            sortOrder = sortOrder == "asc" ? "desc" : "asc";
        }
    
        _param.OrderBy = orderBy + " " + sortOrder; // menambahkan urutan sortir baru ke parameter
        await Get();
    }
    
    private async Task PageSizeChanged(int page)
    {
        _param.PageSize = page;
        await Get();
    } 
    
    private async Task SearchChanged(string keyword)
    {
        _param.PageNumber = 1;
        _param.Keyword = keyword;
        await Get();
    }
    
    private async Task ValidateRejectQty()
    {
        var maxQty = (int)(toUpdate.PodeOrderQty - toUpdate.PodeReceivedQty);
        if ((int)toUpdate.PodeRejectedQty > maxQty)
        {
            await Js.InvokeAsync<object>("alert", $"The maximum value is {maxQty}");
            toUpdate.PodeRejectedQty = 0;
        }
    }
    
    private async Task ValidateReceiveQty()
    {
        var maxQty = (int)(toUpdate.PodeOrderQty - toUpdate.PodeRejectedQty);
        if ((int)toUpdate.PodeReceivedQty > maxQty)
        {
            await Js.InvokeAsync<object>("alert", $"The maximum value is {maxQty}");
            toUpdate.PodeReceivedQty = 0;
        }
    }
    
    private static (string, string) GetStatus(int status)
    {
        return status switch
        {
            1 => ("warning-btn", "Pending"),
            2 => ("info-btn", "Approve"),
            3 => ("danger-btn", "Reject"),
            4 => ("secondary-btn", "Receive"),
            5 => ("dark-btn", "Complete")
        };
    }
} 
