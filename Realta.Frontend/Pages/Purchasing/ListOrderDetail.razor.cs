using Microsoft.AspNetCore.Components;
using Realta.Contract.Models;
using Realta.Domain.Entities;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.HttpRepository.Purchasing;

namespace Realta.Frontend.Pages.Purchasing;

public partial class ListOrderDetail
{
    [Parameter]
    public string Id { get; set; }
    
    [Inject]
    public IPurchaseOrderHttpRepository Repo { get; set; }

    public PurchaseOrderDto Header { get; set; }
    public MetaData MetaData { get; set; } = new();

    private PurchaseOrderDetailParameters _param = new();
    public List<PurchaseOrderDetailDto> DataList { get; set; } = new();
    protected async override Task OnInitializedAsync()
    {
        Header = await Repo.GetHeader(Id);
        await Get();
    }

    private async Task SelectedPage(int page)
    {
        _param.PageNumber = page;
        await Get();
    }
    
    private async Task Get()
    {
        var response = await Repo.GetDetails(Id, _param);
        DataList = response.Items;
        MetaData = response.MetaData;
    }
    
    private async Task SearchChanged(string keyword)
    {
        _param.PageNumber = 1;
        _param.Keyword = keyword;
        await Get();
    }
    
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
    
        _param.OrderBy = orderBy + " " + sortOrder; // menambahkan urutan sortir baru ke parameter
        await Get();
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
} 
