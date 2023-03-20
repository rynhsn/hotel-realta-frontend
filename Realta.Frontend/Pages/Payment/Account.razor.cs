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
    
    private List<Decimal> _saldo = new(){ 750000, 568000, 892000, 450000, 815000, 632000, 782000, 852000, 902000, 1000000 };
    
    [Inject]
    public IAccountHttpRepository AccountsRepo { get; set; }
    public List<AccountDto> AccountList { get; set; } = new();
    public List<PaymentDto> PaymentList { get; set; } = new();
   
    protected async override Task OnInitializedAsync()
    {     
        _account.UserId = _userId;
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
        if (account.Type == "payment")
        {
            _account.ExpMonth = null;
            _account.ExpYear = null;
        }
        
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
        if (_account.Type == "payment")
        {
            _account.ExpMonth = null;
            _account.ExpYear = null;
        }
        
        _account.UserId = _userId;
        await AccountsRepo.Update(_account);
        await FetchAccount();
    }
    
    private async Task onCreateConfirmed()
    {
        _account.Saldo = _saldo[new Random().Next(0, 10)];
        if (_account.Type == "payment")
        {
            _account.ExpMonth = null;
            _account.ExpYear = null;
        }
        
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