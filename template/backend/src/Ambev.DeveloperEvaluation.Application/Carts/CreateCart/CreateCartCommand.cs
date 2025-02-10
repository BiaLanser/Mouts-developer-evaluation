using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart
{
    public class CreateCartCommand : IRequest<CreateCartResult>
    {
        public int UserId { get; set; }
        public List<CartProductDto> Products { get; set; } 
    }
}
