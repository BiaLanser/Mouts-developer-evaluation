namespace Ambev.DeveloperEvaluation.Application.Carts
{
    public class CartDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public List<CartProductDto> Products { get; set; } = new List<CartProductDto>();
    }
}
