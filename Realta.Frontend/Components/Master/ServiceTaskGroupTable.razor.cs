using Microsoft.AspNetCore.Components;
using Realta.Contract.Models;

namespace Realta.Frontend.Components.Master
{
    public partial class ServiceTaskGroupTable
    {
        [Parameter]
        public List<ServiceTaskDto> ServiceTask { get; set; }
    }
}
