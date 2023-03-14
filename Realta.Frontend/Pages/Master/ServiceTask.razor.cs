using Microsoft.AspNetCore.Components;
using Realta.Contract.Models;
using Realta.Frontend.HttpRepository.Master.ServiceTask;

namespace Realta.Frontend.Pages.Master
{
    public partial class ServiceTask
    {
        [Inject]
        public  IServiceTaskHttpRepository ServiceTaskRepository { get; set; }
        public  List<ServiceTaskDto> ServiceTasList { get; set; } = new List<ServiceTaskDto>();

        protected async override Task OnInitializedAsync()
        {
            ServiceTasList = await ServiceTaskRepository.GetServiceTask();
        }
    }
}
