using Realta.Contract.Models;

namespace Realta.Frontend.HttpRepository.Master.ServiceTask
{
    public interface IServiceTaskHttpRepository
    {
        Task<List<ServiceTaskDto>> GetServiceTask();
    }
}
