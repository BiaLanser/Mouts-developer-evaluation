namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleRequest
    {
        public int CustomerId { get; set; }
        public string Branch { get; set; }
        public int CartId { get; set; }
    }
}
