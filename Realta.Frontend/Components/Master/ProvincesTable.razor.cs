using Microsoft.AspNetCore.Components;
using Realta.Contract.Models;

namespace Realta.Frontend.Components.Master
{
    public partial class ProvincesTable
    {
        [Parameter]
        public List<ProvincesDto> Provinces { get; set; }
    }
}
