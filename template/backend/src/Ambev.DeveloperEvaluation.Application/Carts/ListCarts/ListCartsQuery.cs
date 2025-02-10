using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.ListCarts
{
    public class ListCartsQuery : IRequest<ListCartsResult>
    {
        public int Page { get; set; }
        public int Size { get; set; }
        public CartSortOrder Order { get; set; }
    }
}
