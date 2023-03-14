using Microsoft.AspNetCore.Components;
using Realta.Contract.Models;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.HttpRepository.Master.PriceItems;

namespace Realta.Frontend.Pages.Master;

public partial class PriceItemsPaging
{
    public List<PriceItemsDto> PriceItemsListPaging { get; set; } = new List<PriceItemsDto>();
    public MetaData MetaData { get; set; } = new MetaData();

    private PriceItemsParameters _priceItemsParameters = new PriceItemsParameters();
    
    [Inject]
    public IPriceItemsHttpRepository PriceItemsRepository { get; set; }


    protected async override Task OnInitializedAsync()
    {
        await GetPriceItemsPaging();
    }

    private async Task SelectedPage(int page)
    {
        _priceItemsParameters.PageNumber = page;
        await GetPriceItemsPaging();
    }

    private async Task GetPriceItemsPaging()
    {
        var pagingResponse = await PriceItemsRepository.GetPriceItemsPaging(_priceItemsParameters);
        PriceItemsListPaging = pagingResponse.Items;
        MetaData = pagingResponse.MetaData;
    }
}