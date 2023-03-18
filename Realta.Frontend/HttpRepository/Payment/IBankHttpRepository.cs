using HotelRealtaPayment.Contract.Models;
using HotelRealtaPayment.Domain.RequestFeatures;
using Realta.Frontend.Features;

namespace Realta.Frontend.HttpRepository.Payment;

public interface IBankHttpRepository
{
    Task<List<BankDto>> GetBanks();
    Task<PagingResponse<BankDto>> GetBanksPaging(BankParameters bankParameters);
    Task Update(BankDto bank);
    Task Create(BankDto bank);
    Task Delete(int id);
}