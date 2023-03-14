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
    public MetaData MetaData { get; set; } = new MetaData();
    private PurchaseOrderParameters _purchaseOrderParameters = new PurchaseOrderParameters();

    protected async override Task OnInitializedAsync()
    {
        // PurchaseOrders = await PurchaseOrderRepo.Get();
        await GetPaging();
    }   

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
    
    private async Task SearchChange(string keyword)
    {
        _purchaseOrderParameters.PageNumber = 1;
        _purchaseOrderParameters.Keyword = keyword;
        await GetPaging();
    }

}
