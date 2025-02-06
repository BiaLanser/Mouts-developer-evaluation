using Ambev.DeveloperEvaluation.Domain.DTOs;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Services
{
    public interface ICartService
    {
        Task<PaginationDto<Cart>> GetAllCarts(int page, int size, CartSortOrder order);
        Task<Cart> GetCartById(int id);
        Task<Cart> AddCart(Cart cart);
        Task<Cart> UpdateCart(int id, Cart cart);
        Task<bool> DeleteCart(int id);
    }
}
