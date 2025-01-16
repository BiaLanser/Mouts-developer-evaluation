using Ambev.DeveloperEvaluation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Services
{
    public interface ICartService
    {
        Task<IEnumerable<Cart>> GetAllCarts();
        Task<Cart> GetCartById(int id);
        Task<Cart> AddCart(Cart cart);
        Task<Cart> UpdateCart(int id, Cart cart);
        Task<bool> DeleteCart(int id);
    }
}
