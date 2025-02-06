﻿using Ambev.DeveloperEvaluation.Domain.DTOs;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Services
{
    public interface IProductService
    {
        Task<PaginationDto<Product>> GetAllProducts(int page, int size, ProductSortOrder order);
        Task<Product> GetProductById(int id);
        Task AddProduct(Product product);
        Task UpdateProduct(Product product);
        Task DeleteProduct(int id);
        Task<IEnumerable<string>> GetCategories();
        Task<PaginationDto<Product>> GetProductByCategory(string category, int page, int size, ProductSortOrder order);
    }
}
