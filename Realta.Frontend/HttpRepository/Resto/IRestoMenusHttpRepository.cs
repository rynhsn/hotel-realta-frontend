using Realta.Contract.Models;

namespace Realta.Frontend.HttpRepository.Resto
{
    public interface IRestoMenusHttpRepository
    {
        Task<List<RestoMenusDto>> GetRestoMenus();

    }
}
