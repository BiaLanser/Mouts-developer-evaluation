using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductQuery, DeleteProductResponse>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<DeleteProductResponse> Handle(DeleteProductQuery request, CancellationToken cancellationToken)
        {
            var validator = new DeleteProductQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var product = await _productRepository.GetProductById(request.Id);

            if (product == null)
                return new DeleteProductResponse { Success = false };

            await _productRepository.DeleteProduct(request.Id);

            return new DeleteProductResponse { Success = true };
        }
    }
}
