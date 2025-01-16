using Ambev.DeveloperEvaluation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Services
{
    public interface ISaleItemService
    {
        Task<IEnumerable<SaleItem>> GetAllSales();
        Task<SaleItem> GetSaleById(int id);
        Task<SaleItem> AddSale(SaleItem saleItem);
        Task<SaleItem> UpdateSale(SaleItem saleItem);
        Task DeleteSale(int id);
    }
}
