using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Realta.Contract.Models;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.HttpRepository.Purchasing;
using Realta.Frontend.Shared;
using Microsoft.JSInterop;


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
        [Parameter]
        public EventCallback<int> OnDeleteConfirmed { get; set; }
        
        private VendorParameters _vendorParameters = new VendorParameters();
        public List<VendorDto> VendorListPaging { get; set; } = new List<VendorDto>();
        public MetaData MetaData { get; set; } = new MetaData();
        protected async override Task OnInitializedAsync()
        {
            await GetVendorPaging();
        }
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
        private async Task SortChanged(string orderBy)
        {
            _vendorParameters.OrderBy = orderBy;
            await GetVendorPaging();
        }
        
        private VendorDto _vendor = new VendorDto();
        private SuccessNotification _notification;
        private async Task Create()
        {
            await VendorRepo.CreateVendor(_vendor);
        }

        [Inject]
        public IJSRuntime JS { get; set; }
        [Parameter]
        public EventCallback<int> OnDeleted { get; set; }
        private async Task DeleteConfirmed(int id)
        {
            await VendorRepo.DeleteVendor(id);
            _vendorParameters.PageNumber = 1;
            await GetVendorPaging();;
            
            // await OnDeleted.InvokeAsync(id);
            // await ShowSuccessModal();
        }
        
        private async Task ShowSuccessModal()
        {
            await JS.InvokeAsync<object>("$('#successModal').modal('show');");
        }

}
}
