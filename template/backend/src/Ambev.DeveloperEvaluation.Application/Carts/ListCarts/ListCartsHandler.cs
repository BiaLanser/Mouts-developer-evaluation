using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.ListCarts
{
    public class ListCartsHandler : IRequestHandler<ListCartsQuery, ListCartsResult>
    {
        private readonly ICartRepository _cartRepository;

        public ListCartsHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<ListCartsResult> Handle(ListCartsQuery request, CancellationToken cancellationToken)
        {
            var carts = await _cartRepository.GetAllCarts();
            var cartList = carts.ToList();

            int totalItems = cartList.Count();
            int totalPages = (int)Math.Ceiling(totalItems / (double)request.Size);

            // Ordenação
            switch (request.Order)
            {
                case CartSortOrder.IdAsc:
                    cartList = cartList.OrderBy(c => c.Id).ToList();
                    break;
                case CartSortOrder.IdDesc:
                    cartList = cartList.OrderByDescending(c => c.Id).ToList();
                    break;
                case CartSortOrder.UserIdAsc:
                    cartList = cartList.OrderBy(c => c.UserId).ToList();
                    break;
                case CartSortOrder.UserIdDesc:
                    cartList = cartList.OrderByDescending(c => c.UserId).ToList();
                    break;
            }

            // Paginação
            var paginatedCarts = cartList
                .Skip((request.Page - 1) * request.Size)
                .Take(request.Size)
                .ToList();

            return new ListCartsResult
            {
                Carts = paginatedCarts,
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = request.Page
            };
        }
    }
}
