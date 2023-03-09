using Realta.Contract.Models.v1.Hotels;

namespace Realta.Frontend.HttpRepository
{
    public interface IHotelsHttpRepository
    {
        Task<List<HotelsDto>> GetHotels();
    }
}
