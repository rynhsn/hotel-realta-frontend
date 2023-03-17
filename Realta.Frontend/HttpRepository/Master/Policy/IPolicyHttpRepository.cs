using Realta.Contract.Models;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.Features;

namespace Realta.Frontend.HttpRepository.Master.Policy
{
    public interface IPolicyHttpRepository
    {
        Task<List<PolicyDto>> GetPolicy();
        Task<PagingResponse<PolicyDto>> GetPolicyPaging(PolicyParameter policyParameter);

        Task CreatePolicy(PolicyCreateDto policyCreateDto);
        Task UpdatePolicy(PolicyDto policyDto);
        Task<PolicyDto> GetPolicyById(int id);

        Task DeletePolicy(int id);
    }
}
