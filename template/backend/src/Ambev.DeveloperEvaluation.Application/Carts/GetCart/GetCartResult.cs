namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart
{
    public class GetCartResult
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public List<CartProductDto> Products { get; set; }
    }
}
