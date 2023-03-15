using Microsoft.AspNetCore.Components;
using Realta.Contract.Models;
using Realta.Frontend.HttpRepository.Payment;

namespace Realta.Frontend.Pages.Payment;

public partial class Fintech
{
    [Inject]
    public IFintechHttpRepository FintechsRepo { get; set; }
    public List<FintechDto> FintechList { get; set; } = new List<FintechDto>();
   
    protected async override Task OnInitializedAsync()
    {
        FintechList = await FintechsRepo.GetFintechs();
    }
}