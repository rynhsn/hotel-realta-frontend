using Microsoft.AspNetCore.Components;
using Realta.Contract.Models;
using Realta.Frontend.HttpRepository.Purchasing;

namespace Realta.Frontend.Pages.Purchasing
{
    public partial class Vendor
    {

        [Inject]
        public IVendorHttpRepository VendorRepo { get; set; }
        public List<VendorDto> VendorList { get; set; } = new List<VendorDto>();

        protected async override Task OnInitializedAsync()
        {
            VendorList = await VendorRepo.GetVendors();
        }
    }
}
