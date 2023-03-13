using Realta.Domain.RequestFeatures;
using Realta.Frontend.Features;
using Realta.Contract.Models;


namespace Realta.Frontend.HttpRepository.Booking
{
    public interface ISpecialOfferHttpRepository
    {
        Task<List<SpecialOffersDto>> GetSpecialOffers();

        Task<PagingResponse<SpecialOffersDto>> GetSpecialOfferPaging(SpecialOfferParameters specialOfferParameters); 
    }
}
