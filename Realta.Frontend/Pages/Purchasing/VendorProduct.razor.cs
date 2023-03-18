using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Realta.Contract.Models;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.HttpRepository.Purchasing;
using Realta.Frontend.Shared;

namespace Realta.Frontend.Pages.Purchasing;

public partial class VendorProduct
{
    [Parameter]
    public int Id { get; set; }
    [Inject]
    public IVendorProductHttpRepository VendproRepo { get; set; }
    public MetaData MetaData { get; set; } = new MetaData();
    private VenproParameters _vendproParameters = new ();
    public List<VendorProductDto> VendorListPaging { get; set; } = new List<VendorProductDto>();
    private string orderBy = ""; // menunjukkan kolom yang diurutkan
    private string sortOrder = "asc"; // menunjukkan urutan sortir (asc atau desc)
    private VendorProductDto _venpro = new VendorProductDto ();
    private SuccessNotification _notification;
    public VendorHeaderDto Header { get; set; } = new();
    private List<StocksDto> stocks = new ();
    
    protected async override Task OnInitializedAsync()
    {
        Header = await VendproRepo.GetHeaderId(Id);
        stocks = await VendproRepo.GetStock();
        await GetVenproPaging();
    }
    
    private async Task GetVenproPaging()
    {
        var pagingRespon = await VendproRepo.GetVenProPaging(_vendproParameters, Id);
        VendorListPaging = pagingRespon.Items;
        MetaData = pagingRespon.MetaData;
    }
    private async Task SelectedPage (int page)
    {
        _vendproParameters.PageNumber = page;
        await GetVenproPaging();
    }
    private async Task SearchChanged(string keyword)
    {
        _vendproParameters.PageNumber = 1;
        _vendproParameters.Keyword = keyword;
        await GetVenproPaging();
    }
    private async Task SortChanged(string columnName)
    {
        if (orderBy != columnName)
        {
            orderBy = columnName;
            sortOrder = "asc";
        }
        else
        {
            sortOrder = sortOrder == "asc" ? "desc" : "asc";
        } 
        _vendproParameters.OrderBy = orderBy + " " + sortOrder;
        Console.WriteLine(sortOrder);
        await GetVenproPaging();
    }
    private async Task Create()
    {
        _venpro.VeproVendorId = Id;
        await VendproRepo.CreateVenpro(_venpro);
        _notification.Show($"/purchasing/vendor-product/{Id}", "berhasil ditambahkan");
        await GetVenproPaging();
    }

    private void Clear()
    {
        _venpro = new ();
    }

}