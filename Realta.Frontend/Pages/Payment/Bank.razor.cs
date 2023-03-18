using System.Dynamic;
using HotelRealtaPayment.Contract.Models;
using HotelRealtaPayment.Domain.RequestFeatures;
using Microsoft.AspNetCore.Components;
using Realta.Frontend.HttpRepository.Payment;

namespace Realta.Frontend.Pages.Payment;

public partial class Bank
{
    private BankParameters _param = new();
    public MetaData MetaData { get; set; } = new();
    
    [Inject]
    public IBankHttpRepository BanksRepo { get; set; }
    public List<BankDto> BanksList { get; set; } = new();
   
    protected async override Task OnInitializedAsync()
    {
        await FetchBank();
    }

    private async Task FetchBank()
    {
        var response = await BanksRepo.GetBanksPaging(_param);
        BanksList = response.Items;
        MetaData = response.MetaData;
    }
    
    private async Task SelectedPage(int page)
    {
        _param.PageNumber = page;
        await FetchBank();
    }

    private async Task SearchChanged(string keyword)
    {
        _param.PageNumber = 1;
        _param.SearchTerm = keyword;
        await FetchBank();
    }
    
    private async Task PageSizeChanged(int size)
    {
        _param.PageNumber = 1;
        _param.PageSize = size;
        await FetchBank();
    }
}