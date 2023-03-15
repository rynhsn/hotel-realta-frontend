using Realta.Contract.Models;

namespace Realta.Frontend.HttpRepository.Payment;

public interface ITransactionHttpRepository
{
    Task<List<TransactionDto>> GetTransactions();
}