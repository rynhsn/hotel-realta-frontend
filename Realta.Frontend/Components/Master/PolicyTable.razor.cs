using Microsoft.AspNetCore.Components;
using Realta.Contract.Models;

namespace Realta.Frontend.Components.Master
{
    public partial class PolicyTable
    {
        [Parameter]
        public List<PolicyDto> Policy { get; set; }
    }
}
