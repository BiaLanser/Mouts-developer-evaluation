using Ambev.DeveloperEvaluation.Domain.DTOs;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProduct
{
    public class ListProductHandler : IRequestHandler<ListProductQuery, ListProductResult>
    {
        private readonly IProductRepository _productRepository;

        public ListProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ListProductResult> Handle(ListProductQuery command, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllProducts();
            var productList = products.ToList();

            int totalItems = products.Count();
            int totalPages = (int)Math.Ceiling(totalItems / (double)command.Size);

            // Ordenação
            switch (command.Order)
            {
                case ProductSortOrder.IdAsc:
                    productList = productList.OrderBy(c => c.Id).ToList();
                    break;
                case ProductSortOrder.IdDesc:
                    productList = productList.OrderByDescending(c => c.Id).ToList();
                    break;
                case ProductSortOrder.PriceAsc:
                    productList = productList.OrderBy(c => c.Price).ToList();
                    break;
                case ProductSortOrder.PriceDesc:
                    productList = productList.OrderByDescending(c => c.Price).ToList();
                    break;
                case ProductSortOrder.TitleAsc:
                    productList = productList.OrderBy(c => c.Title).ToList();
                    break;
                case ProductSortOrder.TitleDesc:
                    productList = productList.OrderByDescending(c => c.Title).ToList();
                    break;
            }

            // Paginação
            var paginatedProducts = products
                .Skip((command.Page - 1) * command.Size)
                .Take(command.Size)
                .ToList();

            return new ListProductResult
            {
                Products = productList,
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = command.Page
            };
        }
    }
}
