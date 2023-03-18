using HotelRealtaPayment.Contract.Models;
using HotelRealtaPayment.Domain.RequestFeatures;
using Realta.Frontend.Features;

namespace Realta.Frontend.HttpRepository.Payment;

public interface IFintechHttpRepository
{
    Task<List<FintechDto>> GetFintechs();
    Task<PagingResponse<FintechDto>> GetFintechsPaging(FintechParameters fintechParameters);
}