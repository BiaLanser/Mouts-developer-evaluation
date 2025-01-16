using Ambev.DeveloperEvaluation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Services
{
    public interface ISaleService
    {
        Task<IEnumerable<Sale>> GetAllSales();
        Task<Sale> GetSaleById(int id);
        Task<Sale> AddSale(Sale sale);
        Task<Sale> UpdateSale(Sale sale);
        Task DeleteSale(int id);
    }
}
