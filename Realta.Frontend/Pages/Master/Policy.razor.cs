using Microsoft.AspNetCore.Components;
using Realta.Contract.Models;
using Realta.Frontend.HttpRepository.Master.Policy;

namespace Realta.Frontend.Pages.Master
{
    public partial class Policy
    {
        [Inject]
        public  IPolicyHttpRepository PolicyRepository { get; set; }
        public  List<PolicyDto> PolicyList { get; set; } = new List<PolicyDto>();

        protected async override Task OnInitializedAsync()
        {
            PolicyList = await PolicyRepository.GetPolicy();

        }
    }
}
