namespace Ambev.DeveloperEvaluation.Application.Carts
{
    public class CreateCartDto
    {
        public int UserId { get; set; }
        public List<CartProductDto> Products { get; set; } = new List<CartProductDto>();
    }
}
