using Ambev.DeveloperEvaluation.Domain.Entities;

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
