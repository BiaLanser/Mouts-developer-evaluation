using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface ICartRepository
    {
        Task<IEnumerable<Cart>> GetAllCarts();
        Task<Cart> GetCartById(int id);
        Task<Cart> AddCart(Cart cart);
        Task<Cart> UpdateCart(int id, Cart cart);
        Task<bool> DeleteCart(int id);
    }
}
