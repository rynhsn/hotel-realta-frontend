using Realta.Contract.Models;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.Features;

namespace Realta.Frontend.HttpRepository.Resto
{
    public interface IRestoMenusHttpRepository
    {
        Task<List<RestoMenusDto>> GetRestoMenus();
        Task<PagingResponse<RestoMenusDto>> GetPaging(RestoMenusParameters restoMenuParameters);
    }
}
    