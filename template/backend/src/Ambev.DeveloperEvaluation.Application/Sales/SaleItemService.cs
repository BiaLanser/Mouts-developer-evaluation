using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales
{
    internal class SaleItemService
    {
    }
}


/*
 public class SaleItemService : ISaleItemService
{
    private readonly ISaleItemRepository _saleItemRepository;
    private readonly IProductRepository _productRepository;

    public SaleItemService(ISaleItemRepository saleItemRepository, IProductRepository productRepository)
    {
        _saleItemRepository = saleItemRepository;
        _productRepository = productRepository;
    }

    public async Task<SaleItem> AddSaleItem(SaleItem saleItem)
    {
        var product = await _productRepository.GetProductById(saleItem.ProductId);
        if (product == null)
            throw new KeyNotFoundException("Product not found.");

        if (saleItem.Quantity > 20)
            throw new InvalidOperationException("Cannot sell more than 20 identical items.");

        saleItem.TotalAmount = saleItem.Quantity * product.Price;

        return await _saleItemRepository.AddSaleItem(saleItem);
    }

    public async Task<SaleItem> UpdateSaleItem(SaleItem saleItem)
    {
        var existingSaleItem = await _saleItemRepository.GetSaleItemById(saleItem.Id);
        if (existingSaleItem == null)
            throw new KeyNotFoundException("Sale Item not found.");

        existingSaleItem.Quantity = saleItem.Quantity;
        existingSaleItem.TotalAmount = saleItem.Quantity * existingSaleItem.Product.Price;

        return await _saleItemRepository.UpdateSaleItem(existingSaleItem);
    }

    public async Task DeleteSaleItem(int saleItemId)
    {
        var saleItem = await _saleItemRepository.GetSaleItemById(saleItemId);
        if (saleItem == null)
            throw new KeyNotFoundException("Sale Item not found.");

        await _saleItemRepository.DeleteSaleItem(saleItemId);
    }

    public async Task<SaleItem> GetSaleItemById(int saleItemId)
    {
        var saleItem = await _saleItemRepository.GetSaleItemById(saleItemId);
        if (saleItem == null)
            throw new KeyNotFoundException("Sale Item not found.");

        return saleItem;
    }

    public async Task<IEnumerable<SaleItem>> GetAllSaleItems()
    {
        return await _saleItemRepository.GetAllSaleItems();
    }
}

 
 */