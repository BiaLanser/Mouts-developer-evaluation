using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart
{
    public class UpdateCartCommand : IRequest<UpdateCartResult>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<CartProductDto> Products { get; set; }
    }
}
