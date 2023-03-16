using HotelRealtaPayment.Contract.Models;
using Microsoft.AspNetCore.Components;
using Realta.Frontend.HttpRepository.Payment;

namespace Realta.Frontend.Pages.Payment;

public partial class Bank
{
    [Inject]
    public IBankHttpRepository BanksRepo { get; set; }
    public List<BankDto> BanksList { get; set; } = new List<BankDto>();
   
    protected async override Task OnInitializedAsync()
    {
        BanksList = await BanksRepo.GetBanks();
    }
}