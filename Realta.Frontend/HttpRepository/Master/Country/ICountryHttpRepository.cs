using Realta.Contract.Models;

namespace Realta.Frontend.HttpRepository.Master
{
    public interface ICountryHttpRepository
    {
        Task<List<CountryDto>> GetCountry();

    }
}
