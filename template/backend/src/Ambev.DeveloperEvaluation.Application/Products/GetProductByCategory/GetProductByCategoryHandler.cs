using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Enums;
using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProductByCategory
{
    public class GetProductByCategoryHandler : IRequestHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductByCategoryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetProductByCategoryQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var products = await _productRepository.GetProductByCategory(request.Category);
            var productList = products.ToList();

            int totalItems = productList.Count();
            int totalPages = (int)Math.Ceiling(totalItems / (double)request.Size);

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
            var paginatedProducts = products
                .Skip((request.Page - 1) * request.Size)
                .Take(request.Size)
                .ToList();

            return new GetProductByCategoryResult
            {
                Products = productList,
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = request.Page
            };
        }
    }
}
