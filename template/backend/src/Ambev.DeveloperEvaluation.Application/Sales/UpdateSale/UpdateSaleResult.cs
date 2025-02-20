namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleResult
    {
        public int SaleNumber { get; set; }
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }
        public string Branch { get; set; }
        public bool IsCancelled { get; set; }
        public List<SaleItemDTO> SaleItems { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalSaleAmount { get; set; }
    }
}
