using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SaleNumber { get; set; }
        public DateTime SaleDate { get; set; }
        public int CustomerId { get; set; }
        public string Branch { get; set; }
        public bool IsCancelled { get; set; }
        public decimal Discount { get; set; }
        public int CartId { get; set; }
        public List<SaleItem> Items { get; set; } = new List<SaleItem>();
        public decimal TotalSaleAmount { get; set; }

        [ValidateNever]
        public Cart Cart { get; set; }
        //public decimal TotalAmountWithoutDiscount { get; set; }

    }
}
