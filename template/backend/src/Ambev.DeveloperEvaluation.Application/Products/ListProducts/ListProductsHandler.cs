using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProduct
{
    public class ListProductsHandler : IRequestHandler<ListProductsQuery, ListProductsResult>
    {
        private readonly IProductRepository _productRepository;

        public ListProductsHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ListProductsResult> Handle(ListProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllProducts();
            var productList = products.ToList();

            int totalItems = productList.Count();
            int totalPages = (int)Math.Ceiling(totalItems / (double)request.Size);

            // Ordenação
            switch (request.Order)
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
            var paginatedProducts = productList
                .Skip((request.Page - 1) * request.Size)
                .Take(request.Size)
                .ToList();

            return new ListProductsResult
            {
                Products = paginatedProducts,
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = request.Page
            };
        }
    }
}
