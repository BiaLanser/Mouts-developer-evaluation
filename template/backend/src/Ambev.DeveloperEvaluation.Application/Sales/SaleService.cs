using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales
{
    public class SaleService : ISaleService
    {

        private readonly ISaleRepository _saleRepository;
        private readonly IProductRepository _productRepository;
        private readonly ILogger<SaleService> _logger;

        public SaleService(ISaleRepository saleRepository, IProductRepository productRepository, ILogger<SaleService> logger)
        {
            _saleRepository = saleRepository;
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task<Sale> AddSale(Sale sale)
        {
            try
            {
                if (sale.Items == null || !sale.Items.Any())
                    throw new InvalidOperationException("Sale must contain at least one item");

                decimal totalSaleAmount = 0;
                decimal totalDiscount = 0;
                                                           
                foreach (var saleItem in sale.Items)
                {
                    var product = await _productRepository.GetProductById(saleItem.ProductId);
                                  
                    if (saleItem.Quantity > 20)
                        throw new InvalidOperationException("You can only select up to 20 items");

                    saleItem.UnitPrice = product.Price;
                    saleItem.ProductName = product.Title;
                }

                (totalSaleAmount, totalDiscount) = CalculateDiscounts(sale.Items);

                sale.TotalSaleAmount = totalSaleAmount;
                sale.Discount = totalDiscount;

                var createdSale = await _saleRepository.AddSale(sale);
                return createdSale;
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error to update database");
                if (dbEx.InnerException != null)
                {
                    _logger.LogError(dbEx.InnerException, "Inner exception details:");
                }
                throw;
            }
            
        }


        public async Task DeleteSale(int id)
        {
            var sale = await _saleRepository.GetSaleBySaleNumber(id);
            if (sale == null)
                throw new KeyNotFoundException($"Sale with ID: {id} not found");
            await _saleRepository.DeleteSale(id);
        }

        public async Task<IEnumerable<Sale>> GetAllSales()
        {
            return await _saleRepository.GetAllSales();
        }

        public async Task<Sale> GetSaleBySaleNumber(int id)
        {
            var sale = await _saleRepository.GetSaleBySaleNumber(id);
            if (sale == null)
                throw new KeyNotFoundException($"Sale with ID: {id} not found");

            return sale;
        }
        public async Task<Sale> UpdateSale(Sale sale)
        {
            var existingSale = await _saleRepository.GetSaleBySaleNumber(sale.SaleNumber);

            if (existingSale == null)
                throw new KeyNotFoundException($"Sale with ID: {sale.SaleNumber} not found");

            existingSale.SaleDate = sale.SaleDate;
            existingSale.Items = sale.Items;

            (decimal totalSaleAmount, decimal totalDiscount) = CalculateDiscounts(sale.Items);

            existingSale.TotalSaleAmount = totalSaleAmount;
            existingSale.Discount = totalDiscount;

            return await _saleRepository.UpdateSale(existingSale);
        }

        public async Task<Sale> CancelSale(int id)
        {
            var sale = await _saleRepository.GetSaleBySaleNumber(id);

            if (sale == null)
                throw new KeyNotFoundException("Sale not found.");

            sale.IsCancelled = true;
            sale.TotalSaleAmount = 0;
            await _saleRepository.UpdateSale(sale);

            return sale;
        }

        private (decimal totalSaleAmount, decimal totalDiscountt) CalculateDiscounts(IEnumerable<SaleItem> saleItems)
        {
            decimal totalSaleAmount = 0;
            decimal totalDiscount = 0;

            foreach (var saleItem in saleItems)
            {
                decimal itemTotalAmount = saleItem.Quantity * saleItem.UnitPrice;

                if (saleItem.Quantity >= 4 && saleItem.Quantity <= 9)
                {
                    saleItem.TotalAmount = itemTotalAmount * 0.9m;
                    totalDiscount += itemTotalAmount * 0.1m;
                }
                else if (saleItem.Quantity >= 10 && saleItem.Quantity <= 20)
                {
                    saleItem.TotalAmount = itemTotalAmount * 0.8m;
                    totalDiscount += itemTotalAmount * 0.2m;
                }
                else
                {
                    saleItem.TotalAmount = itemTotalAmount;
                }

                totalSaleAmount += saleItem.TotalAmount;
            }

            return (Math.Round(totalSaleAmount, 2), Math.Round(totalDiscount, 2));
        }
    }
}
