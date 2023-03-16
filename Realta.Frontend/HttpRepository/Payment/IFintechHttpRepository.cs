using HotelRealtaPayment.Contract.Models;

namespace Realta.Frontend.HttpRepository.Payment;

public interface IFintechHttpRepository
{
    Task<List<FintechDto>> GetFintechs();
}