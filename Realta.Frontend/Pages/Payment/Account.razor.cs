using HotelRealtaPayment.Contract.Models;
using HotelRealtaPayment.Domain.RequestFeatures;
using Microsoft.AspNetCore.Components;
using Realta.Frontend.HttpRepository.Payment;

namespace Realta.Frontend.Pages.Payment;

public partial class Account
{
    private AccountParameters _param = new();
    public MetaData MetaData { get; set; } = new();
    
    [Inject]
    public IAccountHttpRepository AccountsRepo { get; set; }
    public List<AccountDto> AccountList { get; set; } = new List<AccountDto>();
   
    protected async override Task OnInitializedAsync()
    {
        await FetchAccount();
    }
    
    private async Task FetchAccount()
    {
        var response = await AccountsRepo.GetAccountsPaging(_param);
        AccountList = response.Items;
        MetaData = response.MetaData;
    }
    
    private async Task SelectedPage(int page)
    {
        _param.PageNumber = page;
        await FetchAccount();
    }

    private async Task SearchChanged(string keyword)
    {
        _param.PageNumber = 1;
        _param.SearchTerm = keyword;
        await FetchAccount();
    }
    
    private async Task PageSizeChanged(int size)
    {
        _param.PageNumber = 1;
        _param.PageSize = size;
        await FetchAccount();
    }
}