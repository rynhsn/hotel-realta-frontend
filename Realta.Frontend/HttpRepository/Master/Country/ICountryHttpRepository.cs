using Realta.Contract.Models;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.Features;

namespace Realta.Frontend.HttpRepository.Master
{
    public interface ICountryHttpRepository
    {
        Task<List<CountryDto>> GetCountry();

        Task<PagingResponse<CountryDto>> GetCountryPaging(CountryParameters countryParameters);
    }
}
