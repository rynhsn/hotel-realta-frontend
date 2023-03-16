using HotelRealtaPayment.Contract.Models;
using HotelRealtaPayment.Domain.RequestFeatures;
using Realta.Frontend.Features;

namespace Realta.Frontend.HttpRepository.Payment;

public interface ITransactionHttpRepository
{
    Task<List<TransactionDto>> GetTransactions();
    Task<PagingResponse<TransactionDto>> GetTransactionPaging(TransactionParameters transactionParameters);
}