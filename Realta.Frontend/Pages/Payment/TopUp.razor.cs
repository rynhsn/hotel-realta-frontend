using System.Globalization;
using HotelRealtaPayment.Contract.Models;
using HotelRealtaPayment.Domain.Dto;
using Microsoft.AspNetCore.Components;
using Realta.Frontend.HttpRepository.Payment;
namespace Realta.Frontend.Pages.Payment;

public partial class TopUp
{
    private List<string> _bankTypes = new(){"debet", "credit_card"};
    private TransactionTopUpDto _topUp = new();
    public string? FintechSaldo { get; set; } = "0";
    public string? BankSaldo { get; set; } = "0";


    [Inject]
    public IAccountHttpRepository AccountRepo { get; set; }
    public IEnumerable<AccountUser> AccountBankList { get; set; } = new List<AccountUser>();
    public IEnumerable<AccountUser> AccountFintechList { get; set; } = new List<AccountUser>();

    public int UserId = 4;

    protected async override Task OnInitializedAsync()
    {
        var accountList = await AccountRepo.GetAccountInfo(UserId);
        AccountBankList = accountList.Where(a => _bankTypes.Contains(a.Type));
        AccountFintechList = accountList.Where(a => a.Type == "payment");
        _topUp.UserId = UserId;
    }

    public void SelectedFintech(ChangeEventArgs e)
    {
        var selected = AccountFintechList.FirstOrDefault(f => f.AccountNumber.Equals(e.Value?.ToString()));
        FintechSaldo = selected != null ? $"Rp. {selected.Saldo:#,0}": "";
        _topUp.TargetAccount = (selected != null) ? selected.AccountNumber : "";
        Console.WriteLine(_topUp.TargetAccount);
    }
    public void SelectedBank(ChangeEventArgs e)
    {
        var selected = AccountBankList.FirstOrDefault(b => b.AccountNumber.Equals(e.Value?.ToString()));
        BankSaldo = selected != null ? $"Rp. {selected.Saldo:#,0}": "";
        _topUp.SourceAccount = (selected != null) ? selected.AccountNumber : "";
        Console.WriteLine(_topUp.SourceAccount);
    }
    
    public async Task TopUpFintech()
    {
        var response = await AccountRepo.TopUpFintech(_topUp);
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("Success");
        }
        else
        {
            Console.WriteLine("Failed");
        }
    }
}