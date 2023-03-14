using Realta.Contract.Models;

namespace Realta.Frontend.HttpRepository.Master.Policy
{
    public interface IPolicyHttpRepository
    {
        Task<List<PolicyDto>> GetPolicy();
    }
}
