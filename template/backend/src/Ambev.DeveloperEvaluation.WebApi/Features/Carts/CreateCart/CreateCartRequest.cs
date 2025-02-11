using Ambev.DeveloperEvaluation.Application.Carts;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart
{
    public class CreateCartRequest
    {
        public int UserId { get; set; }
        public List<CartProductDto> Products { get; set; }
    }
}
