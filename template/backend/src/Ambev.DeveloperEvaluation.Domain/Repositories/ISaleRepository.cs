using Ambev.DeveloperEvaluation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface ISaleRepository
    {
        Task<IEnumerable<Sale>> GetAllSales();
        Task<Sale> GetSaleBySaleNumber(int id);
        Task<Sale> AddSale(Sale sale);
        Task<Sale> UpdateSale(Sale sale);
        Task<bool> DeleteSale(int id);
    }
}
