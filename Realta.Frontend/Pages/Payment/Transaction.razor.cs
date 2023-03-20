using HotelRealtaPayment.Contract.Models;
using HotelRealtaPayment.Domain.RequestFeatures;
using Microsoft.AspNetCore.Components;
using Realta.Frontend.HttpRepository.Payment;

namespace Realta.Frontend.Pages.Payment;

public partial class Transaction
{
    public MetaData MetaData { get; set; } = new();
    private TransactionDto _transaction = new();
    public List<TransactionDto> TransactionList { get; set; } = new List<TransactionDto>();

    public Dictionary<string, string> TransactionType = new() { { "tp", "TopUp" }, { "trb", "Booking" }, { "orm", "Order Menu" }, { "rf", "Refund" }, { "rpy", "Repayment"} };

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
    
    private async Task SearchChanged(string keyword)
    {
        _param.PageNumber = 1;
        _param.SearchTerm = keyword;
        await Get();
    }

    private string orderBy = ""; // menunjukkan kolom yang diurutkan
    private string sortOrder = "asc"; // menunjukkan urutan sortir (asc atau desc)
    
    private async Task SortChanged(string columnName)
    {
        if (orderBy != columnName)
        {
            // kolom baru yang di klik, urutan sortir diatur ulang ke ascending
            orderBy = columnName;
            sortOrder = "asc";
        }
        else
        {
            // kolom yang sama yang di klik, urutan sortir diubah antara ascending dan descending
            sortOrder = sortOrder == "asc" ? "desc" : "asc";
        }
        
        _param.PageNumber = 1;
        _param.OrderBy = $"{orderBy} {sortOrder}"; // menambahkan urutan sortir baru ke parameter
        await Get();
    }

    private async Task PageSizeChanged(int size)
    {
        _param.PageSize = size;
        await Get();
    }
    
    private async Task FilterTypeChanged(string type)
    {
        _param.Type= type;
        await Get();
    }
    
    private async Task Get()
    {
        var response = await TransactionRepo.GetTransactionPaging(_param);
        TransactionList = response.Items;
        MetaData = response.MetaData;
    }
    
    private (string, string) GetStatus(string? status)
    {
        return status switch
        {
            "RF" => ("Refund", "orange-bg text-white" ),
            "ORM" => ("Order Menu", "success-bg text-white"),
            "RPY" => ("Repayment", "warning-bg text-white"),
            "TRB" => ("Booking", "primary-btn"),
            "TP" => ("TopUp", "info-bg text-white"),
            _ => ("", "")
        };
    }

    private void ShowDetail(TransactionDto transaction)
    {
        _transaction = transaction;
    }
}