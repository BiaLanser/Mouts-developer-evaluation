using Ambev.DeveloperEvaluation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProducts();
        //Task<IEnumerable<Product>> GetAllAsync(int page = 1, int size = 10, string order = null); ***OPTIONAL PARAMETERS
        Task<Product> GetproductById(int id);
        Task AddProduct(Product product);
        Task UpdateProduct(Product product);
        Task DeleteProduct(int id);
        Task<IEnumerable<string>> GetCategories(); //categorias
        Task<IEnumerable<Product>> GetProductByCategory();
        //Task<IEnumerable<Product>> GetByCategoryAsync(string category, int page = 1, int size = 10, string order = null);
    }
}
