using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Services
{
    public interface ISaleService
    {
        Task<IEnumerable<Sale>> GetAllSales();
        Task<Sale> GetSaleBySaleNumber(int id);
        Task<Sale> AddSale(Sale sale);
        Task<Sale> UpdateSale(Sale sale);
        Task DeleteSale(int id);
        Task<Sale> CancelSale(int id);
    }
}
