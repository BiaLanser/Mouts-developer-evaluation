using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.Domain.DTOs;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Products
{
    public class ProductService : IProductService
    {

        private readonly IProductRepository _productRepository;
        /*
       public ProductService(IProductRepository productRepository)
       {
           _productRepository = productRepository;
       }

       public async Task AddProduct(Product product)
       {
           if (product.Price <= 0)
           {
               throw new ArgumentException("Price must be greater than zero.");
           }

           else if (product.Rating.Count < 0)
           {
               throw new ArgumentException("Rating count cannot be negative.");
           }

           await _productRepository.AddProduct(product);
       }

       public async Task DeleteProduct(int id)
       {
           await GetProductById(id);
           await _productRepository.DeleteProduct(id);
       }

        public async Task<PaginationDto<Product>> GetAllProducts(int page, int size, ProductSortOrder order)
        {
            var products = await _productRepository.GetAllProducts();
      
            int totalItems = products.Count();
            int totalPages = (int)Math.Ceiling(totalItems / (double)size); //Arredonda para inteiro para cima


            switch (order)
            {
                case ProductSortOrder.IdAsc:
                    products = products.OrderBy(c => c.Id).ToList();
                    break;
                case ProductSortOrder.IdDesc:
                    products = products.OrderByDescending(c => c.Id).ToList();
                    break;
                case ProductSortOrder.PriceAsc:
                    products = products.OrderBy(c => c.Price).ToList();
                    break;
                case ProductSortOrder.PriceDesc:
                    products = products.OrderByDescending(c => c.Price).ToList();
                    break;
                case ProductSortOrder.TitleAsc:
                    products = products.OrderBy(c => c.Title).ToList();
                    break;
                case ProductSortOrder.TitleDesc:
                    products = products.OrderByDescending(c => c.Title).ToList();
                    break;
            }

            var paginatedProducts = products
                .Skip((page - 1) * size) //pula x produtos
                .Take(size)  //pega os proximos x produtos
                .ToList();

            return new PaginationDto<Product>
            {
                Data = paginatedProducts,
                TotalItems = totalItems,
                CurrentPage = page,
                TotalPages = totalPages
            };
        
        }

        public async Task<IEnumerable<string>> GetCategories()
        {
            return await _productRepository.GetCategories();
        }

        public async Task<PaginationDto<Product>> GetProductByCategory(string category, int page, int size, ProductSortOrder order)
        {
            var products = await _productRepository.GetProductByCategory(category);

            int totalItems = products.Count();
            int totalPages = (int)Math.Ceiling(totalItems / (double)size);

            switch (order)
            {
                case ProductSortOrder.IdAsc:
                    products = products.OrderBy(c => c.Id).ToList();
                    break;
                case ProductSortOrder.IdDesc:
                    products = products.OrderByDescending(c => c.Id).ToList();
                    break;
                case ProductSortOrder.PriceAsc:
                    products = products.OrderBy(c => c.Price).ToList();
                    break;
                case ProductSortOrder.PriceDesc:
                    products = products.OrderByDescending(c => c.Price).ToList();
                    break;
                case ProductSortOrder.TitleAsc:
                    products = products.OrderBy(c => c.Title).ToList();
                    break;
                case ProductSortOrder.TitleDesc:
                    products = products.OrderByDescending(c => c.Title).ToList();
                    break;
            }

            var paginatedProducts = products
                .Skip((page - 1) * size)
                .Take(size)
                .ToList();

            return new PaginationDto<Product>
            {
                Data = paginatedProducts,
                TotalItems = totalItems,
                CurrentPage = page,
                TotalPages = totalPages
            };
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
            if (product.Price <= 0)
            {
                throw new ArgumentException("Price must be greater than zero.");
            }

            else if (product.Rating.Count < 0)
            {
                throw new ArgumentException("Rating count cannot be negative.");
            }
            await GetProductById(product.Id);
            await _productRepository.UpdateProduct(product);
        } */

    }
}
