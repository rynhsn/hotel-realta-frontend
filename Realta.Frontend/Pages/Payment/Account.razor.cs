using HotelRealtaPayment.Contract.Models;
using HotelRealtaPayment.Domain.RequestFeatures;
using Microsoft.AspNetCore.Components;
using Realta.Frontend.Components;
using Realta.Frontend.HttpRepository.Payment;
using Realta.Frontend.Shared;

namespace Realta.Frontend.Pages.Payment;

public partial class Account
{
    private int _userId = 4;
    private AccountParameters _param = new();
    public MetaData MetaData { get; set; } = new();
    private AccountDto _account = new();
    private ModalDelete _del;
    private SuccessNotification _notif;
    private bool _expiry = true;
    
    [Inject]
    public IAccountHttpRepository AccountsRepo { get; set; }
    public List<AccountDto> AccountList { get; set; } = new();
    public List<PaymentDto> PaymentList { get; set; } = new();
   
    protected async override Task OnInitializedAsync()
    {
        await FetchAccount();
    }
    
    private async Task FetchAccount()
    {
        await FetchPaymentAll();
        
        Task.Delay(500);

        var response = await AccountsRepo.GetAccountsPaging(_account.UserId, _param);
        AccountList = response.Items;
        MetaData = response.MetaData;
    }
    
    private async Task FetchPaymentAll()
    {
        var response = await AccountsRepo.GetPaymentsAll();
        PaymentList = response;
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
    
    private async Task OnUpdate(AccountDto account)
    {
        _account.Id = account.Id;
        _account.Number = account.Number;
        _account.EntityId = account.EntityId;
        _account.Type = account.Type;
        _account.Saldo = account.Saldo;
        _account.ExpMonth = account?.ExpMonth;
        _account.ExpYear = account?.ExpYear;
        _account.Type = account.Type;
    }
    
    private async Task onUpdateConfirmed()
    {
        _account.UserId = _userId;
        await AccountsRepo.Update(_account);
        await FetchAccount();
    }
    
    private async Task onCreateConfirmed()
    {
        _account.UserId = _userId;
        await AccountsRepo.Create(_account);
        await FetchAccount();
    } 

    
    private async Task OnDelete(int id)
    {
        _del.Show(id, $"Account {id} will be deleted!");
        await Task.Delay(100);
    }


    private async Task OnDeleteConfirmed(object id)
    {
        _del.Hide();
        await AccountsRepo.Delete((int)id);
        _param.PageNumber = 1;
        _notif.Show(NavigationManager.Uri);
        await FetchAccount();
    }
}