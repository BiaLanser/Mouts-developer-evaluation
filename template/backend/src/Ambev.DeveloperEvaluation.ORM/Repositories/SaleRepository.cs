using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly DefaultContext _context;
        private readonly ILogger<SaleRepository> _logger;

        public SaleRepository(DefaultContext context, ILogger<SaleRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Sale> AddSale(Sale sale)
        {
            try  //excliir
            {
                await _context.Sales.AddAsync(sale);
                await _context.SaveChangesAsync();
                return sale;
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Erro ao salvar a venda no banco de dados.");
                if (dbEx.InnerException != null)
                {
                    _logger.LogError(dbEx.InnerException, "Detalhes da exceção interna:");
                }
                throw;
            }
        }

        public async Task<bool> DeleteSale(int id)
        {
            var sale = await GetSaleBySaleNumber(id);
            if (sale != null)
            {
                _context.Sales.Remove(sale);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Sale>> GetAllSales()
        {
            return await _context.Sales
                .Where(s => s.Cart != null)
                .Include(s => s.Items)
                .ToListAsync();
        }

        public async Task<Sale> GetSaleBySaleNumber(int id)
        {
            return await _context.Sales
            .Include(s => s.Items)
            .FirstOrDefaultAsync(s => s.SaleNumber == id);
        }

        public async Task<Sale> UpdateSale(Sale sale)
        {
            _context.Sales.Update(sale);
            await _context.SaveChangesAsync();
            return sale;
        }
    }
}
