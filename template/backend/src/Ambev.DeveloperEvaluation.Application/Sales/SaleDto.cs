using Ambev.DeveloperEvaluation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales
{
    public class SaleDto
    {
        public int SaleNumber { get; set; }
        public DateTime Date { get; set; } 
        public int CustomerId { get; set; }
        public string Branch { get; set; }
        public bool IsCancelled { get; set; } 
        public List<SaleItemDTO> SaleItems { get; set; } = new List<SaleItemDTO>();
        public decimal Discount { get; set; }
        public decimal TotalSaleAmount { get; set; }
    }
}

