using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales
{
    
    public class SaleItemDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } 
        public decimal UnitPrice { get; set; } 
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
    }
    

}
