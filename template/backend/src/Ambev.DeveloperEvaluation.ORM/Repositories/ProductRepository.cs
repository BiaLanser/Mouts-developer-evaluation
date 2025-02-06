using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DefaultContext _context;

        public ProductRepository(DefaultContext context)
        {
            _context = context;
        }

        public async Task AddProduct(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProduct(int id)
        {
            var product = await GetProductById(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<IEnumerable<string>> GetCategories()
        {
            return await _context.Products
                                .Select(p => p.Category)
                                .Distinct()
                                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(String category)
        {
            return await _context.Products
                                 .Where(p => p.Category == category)
                                 .ToListAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task UpdateProduct(Product product)
        {
            var existingProduct = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == product.Id);

            if (existingProduct != null)
            {
                existingProduct.Title = product.Title;
                existingProduct.Price = product.Price;
                existingProduct.Description = product.Description;
                existingProduct.Category = product.Category;
                existingProduct.Image = product.Image;

                // Atualiza o Rating, se necessário
                if (existingProduct.Rating != null && product.Rating != null)
                {
                    existingProduct.Rating.Rate = product.Rating.Rate;
                    existingProduct.Rating.Count = product.Rating.Count;
                }
                else if (product.Rating != null)
                {
                    existingProduct.Rating = product.Rating;
                }
            }
            else
            {
                _context.Products.Update(product);
            }

            await _context.SaveChangesAsync();
        }

    }

}
