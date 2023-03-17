using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Realta.Contract.Models;

namespace Realta.Frontend.Components.Master
{
    public partial class PolicyTable
    {
        [Parameter]
        public List<PolicyDto> Policy { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IJSRuntime Js { get; set; }

        [Parameter]
        public EventCallback<int> OnDeleted { get; set; }

        private void RedirectToUpdate(int id)
        {
            var url = Path.Combine("/updatePolicy/", id.ToString());
            NavigationManager.NavigateTo(url);
        }

        private async Task Delete(int id)
        {
            var servicetask = Policy.FirstOrDefault(p => p.PoliId.Equals(id));
            var confirmed = await Js.InvokeAsync<bool>("confirm", $"Delete Policy {servicetask.PoliName} ?");
            if (confirmed)
            {
                await OnDeleted.InvokeAsync(id);

            }
        }

    }
}
