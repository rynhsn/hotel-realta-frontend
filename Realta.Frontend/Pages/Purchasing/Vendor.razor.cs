using Microsoft.AspNetCore.Components;
using Realta.Contract.Models;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.HttpRepository.Purchasing;

namespace Realta.Frontend.Pages.Purchasing
{
    public partial class Vendor
    {

        [Inject]
        public IVendorHttpRepository VendorRepo { get; set; }
        public List<VendorDto> VendorList { get; set; } = new List<VendorDto>();
        //protected async override Task OnInitializedAsync()
        //{
        //    VendorList = await VendorRepo.GetVendors();
        //}
        protected async override Task OnInitializedAsync()
        {
            await GetVendorPaging();
        }

        private VendorParameters _vendorParameters = new VendorParameters();
        public List<VendorDto> VendorListPaging { get; set; } = new List<VendorDto>();
        
        public MetaData MetaData { get; set; } = new MetaData();    

        private async Task GetVendorPaging()
        {
            var pagingRespon = await VendorRepo.GetVendorPaging(_vendorParameters);
            VendorListPaging = pagingRespon.Items;
            MetaData = pagingRespon.MetaData;
        }
        private async Task SelectedPage (int page)
        {
            _vendorParameters.PageNumber = page;
            await GetVendorPaging();
        }

        private async Task SearchChange(string keyword)
        {
            Console.WriteLine(keyword);
            _vendorParameters.PageNumber = 1;
            _vendorParameters.Keyword = keyword;
            await GetVendorPaging();
        }
    }
}
