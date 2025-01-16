using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class SaleItem
    {
        [JsonIgnore]
        public int Id { get; set; }
        [JsonIgnore]
        public int SaleId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }

        [JsonIgnore]
        [ValidateNever]
        public Sale Sale { get; set; }

        [JsonIgnore]
        [ValidateNever]
        public Product Product { get; set; }
    }
}
