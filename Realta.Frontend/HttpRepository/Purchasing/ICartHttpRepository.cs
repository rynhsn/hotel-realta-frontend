using Realta.Contract.Models;
namespace Realta.Frontend.HttpRepository;

public interface ICartHttpRepository
{
    Task<List<CartDto>?> GetCarts();
}