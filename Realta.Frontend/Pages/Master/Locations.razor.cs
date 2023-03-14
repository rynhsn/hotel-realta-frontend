using Microsoft.AspNetCore.Components;
using Realta.Contract.Models;
using Realta.Frontend.HttpRepository.Master;

namespace Realta.Frontend.Pages.Master
{
    public partial class Locations
    {
        [Inject]
        public IRegionsHttpRepository RegionsRepository { get; set; }
        [Inject]
        public ICountryHttpRepository CountryRepository { get; set; }
        [Inject]
        public  IProvincesHttpRepository ProvincesRepository { get; set; }
        [Inject]
        public  IAddressHttpRepository AddressRepository { get; set; }

        public List<RegionsDto> RegionsList { get; set; } = new List<RegionsDto>();
        public List<CountryDto> CountryList { get; set; } = new List<CountryDto>();
        public List<ProvincesDto> ProvincesList { get; set; } = new List<ProvincesDto>();
        public List<AddressDto> AddressList { get; set; } = new List<AddressDto>();

        protected async override Task OnInitializedAsync()
        {
            RegionsList = await RegionsRepository.GetRegions();
            CountryList = await CountryRepository.GetCountry();
            ProvincesList = await ProvincesRepository.GetProvinces();
            AddressList = await AddressRepository.GetAddress();
        }
    }
}
