using Realta.Contract.Models;
using Realta.Frontend.HttpRepository.Booking;
using Microsoft.AspNetCore.Components;
using Realta.Domain.RequestFeatures;


namespace Realta.Frontend.Pages.Booking
{
    public partial class SpofPages
    {
        private async Task SeacrhChange(string searchTerm)
        {
            Console.WriteLine(searchTerm);
            _specialOfferParameters.PageNumber = 1;
            _specialOfferParameters.SearchTerm = searchTerm;
            await GetSpecialOfferPaging();
        }
        public List<SpecialOffersDto> SpecialOffersList { get; set; } = new List<SpecialOffersDto>();
        
        public MetaData MetaData { get; set; } = new MetaData();
        
        [Inject] 
        public ISpecialOfferHttpRepository SpecialOfferHttpRepository { get; set; }
        
        protected async override Task OnInitializedAsync()
        {
            SpecialOffersList= await SpecialOfferHttpRepository.GetSpecialOffers();
            
            Console.WriteLine("SpecialOffersList");
        }

        private SpecialOfferParameters _specialOfferParameters = new SpecialOfferParameters();

        private async Task GetSpecialOfferPaging()
        {
            var pagingResponse = await SpecialOfferHttpRepository.GetSpecialOfferPaging(_specialOfferParameters);
            SpecialOffersList = pagingResponse.Items;
            MetaData = pagingResponse.MetaData;
        }

        private async Task SelectedPage(int page)
        {
            _specialOfferParameters.PageNumber = page;
            await GetSpecialOfferPaging();
        }
    }
    
}

