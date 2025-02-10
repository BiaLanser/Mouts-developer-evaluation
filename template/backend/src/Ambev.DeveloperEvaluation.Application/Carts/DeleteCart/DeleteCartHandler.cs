using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.DeleteCart
{
    public class DeleteCartHandler : IRequestHandler<DeleteCartQuery, DeleteCartResponse>
    {
        public readonly ICartRepository _cartRepository;

        public DeleteCartHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<DeleteCartResponse> Handle(DeleteCartQuery request, CancellationToken cancellationToken)
        {
            var validator = new DeleteCartQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var isDeleted = await _cartRepository.DeleteCart(request.Id);

            return new DeleteCartResponse { Success = true };
        }
    }
}
