using Microsoft.AspNetCore.Components;
using Realta.Contract.Models;

namespace Realta.Frontend.Components.Master
{
    public partial class CategoryGroupTable
    {
        [Parameter]
        public List<CategoryGroupDto> CategoryGroups { get; set; }
    }
}
