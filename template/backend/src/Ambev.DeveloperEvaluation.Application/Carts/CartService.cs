using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Carts;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;

    public CartService(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }
    public async Task<Cart> AddCart(Cart cart)
    {
        return await _cartRepository.AddCart(cart);
    }

    public async Task<bool> DeleteCart(int id)
    {
        return await _cartRepository.DeleteCart(id);
    }

    public async Task<IEnumerable<Cart>> GetAllCarts()
    {
        return await _cartRepository.GetAllCarts();
    }

    public async Task<Cart> GetCartById(int id)
    {
        return await _cartRepository.GetCartById(id);
    }

    public async Task<Cart> UpdateCart(int id, Cart cart)
    {
        return await _cartRepository.UpdateCart(id, cart);
    }
}
