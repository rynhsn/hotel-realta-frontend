using Realta.Contract.Models;

namespace Realta.Frontend.HttpRepository.Payment;

public interface IAccountHttpRepository
{
    public Task<List<AccountDto>> GetAccounts();
}