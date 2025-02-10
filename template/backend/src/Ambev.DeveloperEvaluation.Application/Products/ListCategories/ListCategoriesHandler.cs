using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.ListCategories
{
    public class ListCategoriesHandler : IRequestHandler<ListCategoriesQuery, ListCategoriesResult>
    {
        private readonly IProductRepository _productRepository;

        public ListCategoriesHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ListCategoriesResult> Handle(ListCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _productRepository.GetCategories();

            return new ListCategoriesResult
            {
                Categories = categories.ToList()
            };
        }
    }
}
