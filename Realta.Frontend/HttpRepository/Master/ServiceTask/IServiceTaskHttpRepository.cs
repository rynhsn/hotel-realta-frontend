using Realta.Contract.Models;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.Features;

namespace Realta.Frontend.HttpRepository.Master.ServiceTask
{
    public interface IServiceTaskHttpRepository
    {
        Task<List<ServiceTaskDto>> GetServiceTask();
        Task<PagingResponse<ServiceTaskDto>> GetServiceTaskPaging(ServiceTaskParameter serviceTaskParameter);


        Task CreateServiceTask(ServiceTaskCreateDto serviceTaskCreateDto);
        Task UpdateServiceTask(ServiceTaskDto serviceTaskDto);
        Task<ServiceTaskDto> GetServiceTaskById(int id);

        Task deleteServiceTask(int id);

    }
}
