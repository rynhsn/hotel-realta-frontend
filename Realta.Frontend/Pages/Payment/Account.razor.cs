using Microsoft.AspNetCore.Components;
using Realta.Contract.Models;
using Realta.Frontend.HttpRepository.Payment;

namespace Realta.Frontend.Pages.Payment;

public partial class Account
{
    [Inject]
    public IAccountHttpRepository AccountsRepo { get; set; }
    public List<AccountDto> AccountList { get; set; } = new List<AccountDto>();
   
    protected async override Task OnInitializedAsync()
    {
        AccountList = await AccountsRepo.GetAccounts();
    }
}