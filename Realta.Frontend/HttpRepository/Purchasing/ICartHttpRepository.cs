using Realta.Contract.Models;

namespace Realta.Frontend.HttpRepository.Purchasing;

public interface ICartHttpRepository
{
    Task<List<CartDto>> Get(int empId);
    Task Create(CartDto data);
    Task Update(CartDto data);
    Task Delete(CartDto data);
}