using Microsoft.AspNetCore.Components;
using Realta.Contract.Models;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.HttpRepository.Purchasing;

namespace Realta.Frontend.Pages.Purchasing;

public partial class ListOrder
{

    [Inject]
    public IPurchaseOrderHttpRepository PurchaseOrderRepo { get; set; }
    public List<PurchaseOrderDto> PurchaseOrders { get; set; } = new List<PurchaseOrderDto>();

    protected async override Task OnInitializedAsync()
    {
        // PurchaseOrders = await PurchaseOrderRepo.Get();
        Console.WriteLine(_purchaseOrderParameters.OrderBy);
        await GetPaging();
    }
    
    private PurchaseOrderParameters _purchaseOrderParameters = new PurchaseOrderParameters();
    public MetaData MetaData { get; set; } = new MetaData();

    private async Task SelectedPage(int page)
    {
        _purchaseOrderParameters.PageNumber = page;
        await GetPaging();
    }
    
    private async Task GetPaging()
    {
        var response = await PurchaseOrderRepo.GetPaging(_purchaseOrderParameters);
        PurchaseOrders = response.Items;
        MetaData = response.MetaData;
    }
    
    public static (string, string) GetStatus(int status)
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
    
    private async Task SearchChanged(string keyword)
    {
        _purchaseOrderParameters.PageNumber = 1;
        _purchaseOrderParameters.Keyword = keyword;
        await GetPaging();
    }
    // private async Task SortChanged(string orderBy)
    // {
    //     Console.WriteLine(_purchaseOrderParameters.OrderBy);
    //     _purchaseOrderParameters.OrderBy = orderBy;
    //     await GetPaging();
    // }
    
    private string orderBy = ""; // menunjukkan kolom yang diurutkan
    private string sortOrder = "asc"; // menunjukkan urutan sortir (asc atau desc)
    
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
    
        _purchaseOrderParameters.OrderBy = orderBy + " " + sortOrder; // menambahkan urutan sortir baru ke parameter
        await GetPaging();
    }
}
