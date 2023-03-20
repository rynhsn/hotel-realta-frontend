using Realta.Contract.Models;

namespace Realta.Frontend.HttpRepository.Resto
{
    public interface IOrderMenusHttpRepository
    {
        Task<List<OrderMenusDto>> GetOrderMenus();
    }
}
