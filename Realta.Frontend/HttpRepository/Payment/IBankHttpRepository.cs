using HotelRealtaPayment.Contract.Models;

namespace Realta.Frontend.HttpRepository.Payment;

public interface IBankHttpRepository
{
    Task<List<BankDto>> GetBanks();
}