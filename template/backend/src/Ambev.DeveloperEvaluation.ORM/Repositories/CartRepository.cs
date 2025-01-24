using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly DefaultContext _context;

        public CartRepository(DefaultContext context)
        {
            _context = context;
        }

        public async Task<Cart> AddCart(Cart cart)
        {
            await _context.Carts.AddAsync(cart);

            if (cart.Products != null && cart.Products.Any())
            {
                foreach (var cartProduct in cart.Products)
                {
                    cartProduct.CartId = cart.Id;
                }
            }

            await _context.SaveChangesAsync();
            return cart;
        }

        public async Task<bool> DeleteCart(int id)
        {
            var cart = await _context.Carts.FindAsync(id);
            if (cart == null) return false;

            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Cart>> GetAllCarts()
        {
            return await _context.Carts
                             .Where(c => c.Products.Any())
                             .Include(c => c.Products)
                             .ToListAsync();
        }

        public async Task<Cart> GetCartById(int id)
        {
            return await _context.Carts
                             .Include(c => c.Products)
                             .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Cart> UpdateCart(int id, Cart cart)
        {
            var existingCart = await _context.Carts
                .Include(c => c.Products) 
                .FirstOrDefaultAsync(c => c.Id == id);

            if (existingCart != null)
            {
                existingCart.UserId = cart.UserId;
                existingCart.Date = cart.Date;

                if (cart.Products != null && cart.Products.Any())
                {
                    foreach (var cartProduct in cart.Products)
                    {
                        cartProduct.CartId = id;
                    }

                    existingCart.Products = cart.Products;
                }

                await _context.SaveChangesAsync();
                return existingCart;
            }

            return null;
        }
    }
}
