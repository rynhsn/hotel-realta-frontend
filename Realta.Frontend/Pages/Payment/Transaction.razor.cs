using HotelRealtaPayment.Contract.Models;
using HotelRealtaPayment.Domain.RequestFeatures;
using Microsoft.AspNetCore.Components;
using Realta.Frontend.HttpRepository.Payment;

namespace Realta.Frontend.Pages.Payment;

public partial class Transaction
{
    public MetaData MetaData { get; set; } = new();
    public List<TransactionDto> TransactionList { get; set; } = new List<TransactionDto>();
    
    private TransactionParameters _param = new();
    [Inject] public ITransactionHttpRepository TransactionRepo { get; set; }
    
    protected async override Task OnInitializedAsync()
    {
        // TransactionList = await TransactionRepo.GetTransactions();
        await Get();
    }


    private async Task SelectedPage(int page)
    {
        _param.PageNumber = page;
        await Get();
    }
    
    private async Task Get()
    {
        var response = await TransactionRepo.GetTransactionPaging(_param);
        TransactionList = response.Items;
        MetaData = response.MetaData;
    }
}