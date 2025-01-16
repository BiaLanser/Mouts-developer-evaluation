using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class SaleItemRepository : ISaleItemRepository
    {
        private readonly DefaultContext _context;

        public SaleItemRepository(DefaultContext context)
        {
            _context = context;
        }
        public async Task<SaleItem> AddSaleItem(SaleItem saleItem)
        {
            await _context.SaleItems.AddAsync(saleItem);
            await _context.SaveChangesAsync();
            return saleItem;
        }

        public async Task<bool> DeleteSale(int id)
        {
            var saleItem = await _context.SaleItems
            .FirstOrDefaultAsync(si => si.Id == id);

            if (saleItem != null)
            {
                _context.SaleItems.Remove(saleItem);
                await _context.SaveChangesAsync();
                return true; 
            }

            return false;
        }

        public async Task<IEnumerable<SaleItem>> GetAllSaleItem()
        {
            return await _context.SaleItems
                .Include(si => si.Product)  
                .Include(si => si.Sale)    
                .ToListAsync();
        }

        public async Task<SaleItem> GetSaleItemById(int id)
        {
            return await _context.SaleItems
                .Include(si => si.Product)
                .Include(si => si.Sale)
                .FirstOrDefaultAsync(si => si.Id == id);
        }

        public async Task<SaleItem> UpdateSale(SaleItem saleItem)
        {
            var existingSaleItem = await _context.SaleItems
           .FirstOrDefaultAsync(si => si.Id == saleItem.Id);

            if (existingSaleItem != null)
            {
                existingSaleItem.Quantity = saleItem.Quantity;
                existingSaleItem.UnitPrice = saleItem.UnitPrice;
                existingSaleItem.TotalAmount = saleItem.TotalAmount;

                existingSaleItem.SaleId = saleItem.SaleId;
                existingSaleItem.ProductId = saleItem.ProductId;

                await _context.SaveChangesAsync();
                return existingSaleItem; 
            }

            return null;
        }
    }
}

