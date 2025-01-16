using Ambev.DeveloperEvaluation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface ISaleItemRepository
    {
        Task<IEnumerable<SaleItem>> GetAllSaleItem();
        Task<SaleItem> GetSaleItemById(int id);
        Task<SaleItem> AddSaleItem(SaleItem saleItem);
        Task<SaleItem> UpdateSale(SaleItem saleItem);
        Task<bool> DeleteSale(int id);
    }
}
