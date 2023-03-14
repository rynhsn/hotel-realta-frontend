using Microsoft.AspNetCore.Components;
using Realta.Contract.Models;

namespace Realta.Frontend.Components.Master
{
    public partial class AddressTable
    {
        [Parameter]
        public List<AddressDto> Address { get; set; }
    }
}
