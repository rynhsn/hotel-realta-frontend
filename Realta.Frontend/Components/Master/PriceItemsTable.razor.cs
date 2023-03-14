using Microsoft.AspNetCore.Components;
using Realta.Contract.Models;

namespace Realta.Frontend.Components.Master
{
    public partial class PriceItemsTable
    {
        [Parameter]
        public List<PriceItemsDto> priceItems { get; set; }
    }
}
