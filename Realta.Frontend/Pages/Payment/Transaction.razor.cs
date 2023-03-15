using Microsoft.AspNetCore.Components;
using Realta.Contract.Models;
using Realta.Frontend.HttpRepository.Payment;

namespace Realta.Frontend.Pages.Payment;

public partial class Transaction
{
    [Inject]
    public ITransactionHttpRepository TransactionRepo { get; set; }
    public List<TransactionDto> TransactionList { get; set; } = new List<TransactionDto>();
   
    protected async override Task OnInitializedAsync()
    {
        TransactionList = await TransactionRepo.GetTransactions();
    }
}