using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class SaleItem
    {
        public int Id { get; set; }
        public int SaleId { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalAmount { get; set; }

        [ValidateNever]
        public Sale Sale { get; set; }

        [ValidateNever]
        public Product Product { get; set; }
    }
}
