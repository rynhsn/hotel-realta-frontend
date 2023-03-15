using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Realta.Contract.Models;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.HttpRepository.Booking;



namespace Realta.Frontend.Components.Booking
{
    public partial class SpofTable
    {   
        [Parameter] public List<SpecialOffersDto> SpecialOffer { get; set; }
        
        [Inject] 
        public ISpecialOfferHttpRepository SpecialOfferHttpRepository { get; set; }
        protected async override Task OnInitializedAsync()
        {
            SpecialOffersList= await SpecialOfferHttpRepository.GetSpecialOffers();
            Console.WriteLine("SpecialOffersList");
        }
        public List<SpecialOffersDto> SpecialOffersList { get; set; } = new List<SpecialOffersDto>();
                
        private SpecialOfferParameters _specialOfferParameters = new SpecialOfferParameters();

        public MetaData MetaData { get; set; } = new MetaData();
        
        private async Task SearchChange(string searchTerm)
        {
            Console.WriteLine(searchTerm);
            _specialOfferParameters.PageNumber = 0;
            _specialOfferParameters.SearchTerm = searchTerm;
            await GetSpecialOfferPaging();
        }
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
