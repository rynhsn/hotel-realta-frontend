using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using HotelRealtaPayment.Contract.Models;
using HotelRealtaPayment.Domain.RequestFeatures;
using Microsoft.AspNetCore.Components;
using Realta.Frontend.Components;
using Realta.Frontend.HttpRepository.Payment;
using Realta.Frontend.Shared;

namespace Realta.Frontend.Pages.Payment;

public partial class Bank
{
    private BankParameters _param = new();
    public MetaData MetaData { get; set; } = new();
    private BankDto _bank = new();
    private ModalDelete _del;
    private SuccessNotification _notif;
    
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

    private async Task OnUpdateBank(BankDto bank)
    {
        _bank.Id = bank.Id;
        _bank.Code = bank.Code;
        _bank.Name = bank.Name;
    }
    
    private async Task onUpdateConfirmed()
    {
        await BanksRepo.UpdateBank(_bank);
        await FetchBank();
    }
    
    private async Task onCreateConfirmed()
    {
        await BanksRepo.CreateBank(_bank);
        await FetchBank();
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
    
    private async Task OnDelete(int id)
    {
        _del.Show(id, $"Purchase Order {id} will be deleted!");
        await Task.Delay(100);
    }


    private async Task OnDeleteConfirmed(object id)
    {
        _del.Hide();
        await BanksRepo.DeleteBank((int)id);
        _param.PageNumber = 1;
        _notif.Show(NavigationManager.Uri);
        await FetchBank();
    }
}