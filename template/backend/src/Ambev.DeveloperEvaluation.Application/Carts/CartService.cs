using Ambev.DeveloperEvaluation.Domain.DTOs;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Carts;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;

    public CartService(ICartRepository cartRepository, IProductRepository productRepository)
    {
        _cartRepository = cartRepository;
        _productRepository = productRepository;
    }
    public async Task<Cart> AddCart(Cart cart)
    {
        foreach (var cartProduct in cart.Products)
        {
            var product = await _productRepository.GetProductById(cartProduct.ProductId);

            if (cartProduct.Quantity <= 0)
                throw new InvalidOperationException($"Product with ID {cartProduct.ProductId} has an invalid quantity.");

            if (product == null)
                throw new InvalidOperationException($"Product with ID {cartProduct.ProductId} not found");

            if (product.Price <= 0)
                throw new InvalidOperationException($"Product with ID {cartProduct.ProductId} has an invalid price");
        }
        if (cart.UserId <= 0)
            throw new InvalidOperationException("Invalid User ID.");


        return await _cartRepository.AddCart(cart);
    }


    public async Task<bool> DeleteCart(int id)
    {
        return await _cartRepository.DeleteCart(id);
    }

    public async Task<PaginationDto<Cart>> GetAllCarts(int page, int size, string order)
    {
        var carts = await _cartRepository.GetAllCarts();

        int totalItems = carts.Count();
        int totalPages = (int)Math.Ceiling(totalItems / (double)size);

        if (order == "id asc")
        {
            carts = carts.OrderBy(c => c.Id).ToList();
        }
        else if (order == "id desc")
        {
            carts = carts.OrderByDescending(c => c.Id).ToList();
        }
        else if (order == "userId asc")
        {
            carts = carts.OrderBy(c => c.UserId).ToList();
        }
        else if (order == "userId desc")
        {
            carts = carts.OrderByDescending(c => c.UserId).ToList();
        }

        var paginatedCarts = carts
            .Skip((page - 1) * size)
            .Take(size)
            .ToList();

        return new PaginationDto<Cart>
        {
            Data = paginatedCarts,
            TotalItems = totalItems,
            CurrentPage = page,
            TotalPages = totalPages
        };
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
