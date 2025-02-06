namespace Ambev.DeveloperEvaluation.Application.Sales
{
    public class CreateSaleDto
    {
        public int CustomerId { get; set; }
        public string Branch { get; set; }
        public int CartId { get; set; }
    }
}
