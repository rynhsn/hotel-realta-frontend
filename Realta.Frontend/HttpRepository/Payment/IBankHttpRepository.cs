using HotelRealtaPayment.Contract.Models;
using HotelRealtaPayment.Domain.RequestFeatures;
using Realta.Frontend.Features;

namespace Realta.Frontend.HttpRepository.Payment;

public interface IBankHttpRepository
{
    Task<List<BankDto>> GetBanks();
    Task<PagingResponse<BankDto>> GetBanksPaging(BankParameters bankParameters);
    Task UpdateBank(BankDto bank);
    Task CreateBank(BankDto bank);
    Task DeleteBank(int id);
}