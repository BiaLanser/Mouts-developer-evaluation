using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart
{
    public class GetCartHandler : IRequestHandler<GetCartQuery, GetCartResult>
    {
        public readonly ICartRepository _cartRepository;
        public readonly IMapper _mapper;

        public GetCartHandler(ICartRepository cartRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }

        public async Task<GetCartResult> Handle(GetCartQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetCartQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var cart = await _cartRepository.GetCartById(request.Id);
            if (cart == null)
                throw new KeyNotFoundException("Cart not found");

            return _mapper.Map<GetCartResult>(cart);
        }
    }
}
