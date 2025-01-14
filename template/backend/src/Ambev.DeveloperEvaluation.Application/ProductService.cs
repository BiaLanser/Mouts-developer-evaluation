using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application
{
    public class ProductService : IProductService
    {

        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task AddProduct(Product product)
        {
            await _productRepository.AddProduct(product);
        }

        public async Task DeleteProduct(int id)
        {
            await GetProductById(id);
            await _productRepository.DeleteProduct(id);
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _productRepository.GetAllProducts();
        }

        public async Task<IEnumerable<string>> GetCategories()
        {
            return await _productRepository.GetCategories();
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string category)
        {
            var products = await _productRepository.GetProductByCategory(category);
            return products.Where(p => p.Category.Equals(category, System.StringComparison.OrdinalIgnoreCase));
        }

        public async Task<Product> GetProductById(int id)
        {
            var product = await _productRepository.GetProductById(id);
            if (product == null)
                throw new KeyNotFoundException("Product not found");
            return product;
        }

        public async Task UpdateProduct(Product product)
        {
            await GetProductById(product.Id);
            await _productRepository.UpdateProduct(product);
        }
    }
}
