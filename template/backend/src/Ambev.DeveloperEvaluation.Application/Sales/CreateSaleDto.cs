using Ambev.DeveloperEvaluation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Application.Sales;

namespace Ambev.DeveloperEvaluation.Application.Sales
{
    public class CreateSaleDto
    {
        public int CustomerId { get; set; }
        public string Branch { get; set; }
        public int CartId { get; set; }
    }
}
