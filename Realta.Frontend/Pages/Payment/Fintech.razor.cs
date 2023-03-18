using HotelRealtaPayment.Contract.Models;
using HotelRealtaPayment.Domain.RequestFeatures;
using Microsoft.AspNetCore.Components;
using Realta.Frontend.HttpRepository.Payment;

namespace Realta.Frontend.Pages.Payment;

public partial class Fintech
{
    private FintechParameters _param = new();
    public MetaData MetaData { get; set; } = new();
    
    [Inject]
    public IFintechHttpRepository FintechsRepo { get; set; }
    public List<FintechDto> FintechList { get; set; } = new List<FintechDto>();
   
    protected async override Task OnInitializedAsync() 
    {
        await FetchFintech();
    }
    
    private async Task FetchFintech()
    {
        var response = await FintechsRepo.GetFintechsPaging(_param);
        FintechList = response.Items;
        MetaData = response.MetaData;
    }
    
    private async Task SelectedPage(int page)
    {
        _param.PageNumber = page;
        await FetchFintech();
    }

    private async Task SearchChanged(string keyword)
    {
        _param.PageNumber = 1;
        _param.SearchTerm = keyword;
        await FetchFintech();
    }
    
    private async Task PageSizeChanged(int size)
    {
        _param.PageNumber = 1;
        _param.PageSize = size;
        await FetchFintech();
    }
}