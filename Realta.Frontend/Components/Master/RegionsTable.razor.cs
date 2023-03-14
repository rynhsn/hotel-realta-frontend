using Microsoft.AspNetCore.Components;
using Realta.Contract.Models;

namespace Realta.Frontend.Components.Master
{
    public partial class RegionsTable
    {
        [Parameter]
        public List<RegionsDto> Regions { get; set; }

    }
}
