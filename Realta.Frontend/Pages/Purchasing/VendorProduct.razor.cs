using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Realta.Contract.Models;
using Realta.Frontend.HttpRepository.Purchasing;

namespace Realta.Frontend.Pages.Purchasing;

public partial class VendorProduct
{
    [Parameter]
    public int Id { get; set; }
    [Inject]
    public IVendorProductHttpRepository VendproRepo { get; set; }
    public List<VendorProductDto> VenproList { get; set; } = new List<VendorProductDto>();

    protected async override Task OnInitializedAsync()
    {
        VenproList = await VendproRepo.GetVendorProduct(Id);
    }




}