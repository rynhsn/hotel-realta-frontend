using Realta.Contract.Models;
using Realta.Domain.Dto;

namespace Realta.Frontend.HttpRepository.Resto
{
    public interface IOrmeDetailHttpRepository
    {
        Task<List<OrderMenusDto>> GetOrderMenus();
        Task CreateDetail(OrderMenusDto ormeDetailDto);

        Task UpdateDetail (OrderMenusDto ormeDetailDto);
        Task CreateDetail(NewOrderMenusDto tocreate);
    }
}
