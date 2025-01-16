using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales
{
    public class SaleService : ISaleService
    {

        private readonly ISaleRepository _saleRepository;
        private readonly ISaleItemRepository _saleItemRepository;
        private readonly IProductRepository _productRepository;

        public SaleService(ISaleRepository saleRepository, ISaleItemRepository saleItemRepository, IProductRepository productRepository)
        {
            _saleRepository = saleRepository;
            _saleItemRepository = saleItemRepository;
            _productRepository = productRepository;
        }
        public async Task<Sale> AddSale(Sale sale)
        {
            decimal totalSaleAmount = 0;

            foreach (var saleItem in sale.Items)
            {
                var product = await _productRepository.GetProductById(saleItem.ProductId);

                if (saleItem.Quantity > 20)
                    throw new InvalidOperationException("You can only select up to 20 items");

                if (saleItem.Quantity >= 4 && saleItem.Quantity <= 9)
                    saleItem.TotalAmount = saleItem.Quantity * product.Price * 0.9m;
                else if (saleItem.Quantity >= 10 && saleItem.Quantity <= 20)
                    saleItem.TotalAmount = saleItem.Quantity * product.Price * 0.8m;
                else
                    saleItem.TotalAmount = saleItem.Quantity * product.Price;

                totalSaleAmount += saleItem.TotalAmount;
            }

            sale.TotalSaleAmount = totalSaleAmount;

            var createdSale = await _saleRepository.AddSale(sale);
            return createdSale;
        }
        
        public async Task DeleteSale(int id)
        {
            var sale = await _saleRepository.GetSaleById(id);
            if (sale == null)
                throw new KeyNotFoundException($"Sale with ID: {id} not found");
            await _saleRepository.DeleteSale(id);
        }

        public async Task<IEnumerable<Sale>> GetAllSales()
        {
            return await _saleRepository.GetAllSales();
        }

        public async Task<Sale> GetSaleById(int id)
        {
            var sale = await _saleRepository.GetSaleById(id);
            if (sale == null)
                throw new KeyNotFoundException($"Sale with ID: {id} not found");

            return sale;
        }

        public async Task<Sale> UpdateSale(Sale sale)
        {
            var existingSale = await _saleRepository.GetSaleById(sale.Id);

            if (existingSale == null)
                throw new KeyNotFoundException($"Sale with ID: {sale.Id} not found");

            existingSale.SaleDate = sale.SaleDate;
            existingSale.Items = sale.Items;
            existingSale.TotalSaleAmount = sale.TotalSaleAmount;

            return await _saleRepository.UpdateSale(existingSale);
        }
    }
}
