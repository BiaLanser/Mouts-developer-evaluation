namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    public class UpdateSaleRequest
    {
        public int SaleNumber { get; set; }
        public int CustomerId { get; set; }
        public string Branch { get; set; }
        public int CartId { get; set; }
    }
}
