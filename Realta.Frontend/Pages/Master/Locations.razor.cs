using Microsoft.AspNetCore.Components;
using Realta.Contract.Models;
using Realta.Domain.RequestFeatures;
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

        private ProvincesParameter _provincesParameter = new ProvincesParameter();
        private RegionsParameter _regionsParameter= new RegionsParameter();
        private CountryParameters _countryParameters= new CountryParameters();
        private AddressParameter _addressParameter= new AddressParameter();

        public MetaData MetaDataR { get; set; } = new MetaData();
        public MetaData MetaDataC { get; set; } = new MetaData();
        public MetaData MetaDataP { get; set; } = new MetaData();
        public MetaData MetaDataA { get; set; } = new MetaData();

        private async Task SelectedPageRegions(int page)
        {
            _regionsParameter.PageNumber = page;
            await GetRegions();
        }
        private async Task SelectedPageCountry(int page)
        {
            _countryParameters.PageNumber = page;
            await GetCountry();
        }
        private async Task SelectedPageProvinces(int page)
        {
            _provincesParameter.PageNumber = page;
            await GetProvinces();
        }
        private async Task SelectedPageAddress(int page)
        {
            _addressParameter.PageNumber = page;
            await GetAddress();
        }


        private async Task GetRegions()
        {
            var response = await RegionsRepository.GetRegionsPaging(_regionsParameter);
            RegionsList = response.Items;
            MetaDataR = response.MetaData;
        }
        private async Task GetCountry()
        {
            var response = await CountryRepository.GetCountryPaging(_countryParameters);
            CountryList = response.Items;
            MetaDataC = response.MetaData;
        }
        private async Task GetProvinces()
        {
            var response = await ProvincesRepository.GetProvincesPaging(_provincesParameter);
            ProvincesList = response.Items;
            MetaDataP = response.MetaData;
        }
        private async Task GetAddress()
        {
            var response = await AddressRepository.GetAddressPaging(_addressParameter);
            AddressList = response.Items;
            MetaDataA = response.MetaData;
        }

        protected async override Task OnInitializedAsync()
        {
            //RegionsList = await RegionsRepository.GetRegions();
            //CountryList = await CountryRepository.GetCountry();
            //ProvincesList = await ProvincesRepository.GetProvinces();
            //AddressList = await AddressRepository.GetAddress();
            await GetRegions();
            await GetCountry();
            await GetProvinces();
            await GetAddress();
        }

        private async Task SearchChangeR(string searchTerm)
        {
            Console.WriteLine(searchTerm);
            _regionsParameter.PageNumber = 1;
            _regionsParameter.SearchTerm = searchTerm;
            await GetRegions();
        }
        private async Task SearchChangeC(string searchTerm)
        {
            Console.WriteLine(searchTerm);
            _countryParameters.PageNumber = 1;
            _countryParameters.SearchTerm = searchTerm;
            await GetCountry();
        }

        private async Task SearchChangeP(string searchTerm)
        {
            Console.WriteLine(searchTerm);
            _provincesParameter.PageNumber = 1;
            _provincesParameter.SearchTerm = searchTerm;
            await GetProvinces();
        }
        private async Task SearchChangeA(string searchTerm)
        {
            Console.WriteLine(searchTerm);
            _addressParameter.PageNumber = 1;
            _addressParameter.SearchTerm = searchTerm;
            await GetAddress();
        }
    }
}
