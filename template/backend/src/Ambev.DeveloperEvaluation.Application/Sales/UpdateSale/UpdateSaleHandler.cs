using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly DiscountCalculator _discountCalculator;

        public UpdateSaleHandler(ISaleRepository saleRepository, ICartRepository cartRepository, IProductRepository productRepository, IMapper mapper, DiscountCalculator discountCalculator)
        {
            _saleRepository = saleRepository;
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _mapper = mapper;
            _discountCalculator = discountCalculator;
        }

        public async Task<UpdateSaleResult> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateSaleCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var sale = _mapper.Map<Sale>(request);
            var existingSale = await _saleRepository.GetSaleBySaleNumber(sale.SaleNumber);

            if (existingSale == null)
                throw new KeyNotFoundException($"Sale with ID: {sale.SaleNumber} not found");


            if (sale.CartId != existingSale.CartId)
            {
                var cart = await _cartRepository.GetCartById(sale.CartId);

                if (cart == null || !cart.Products.Any())
                    throw new InvalidOperationException("Sale must contain at least one item from the cart");

                existingSale.Items = cart.Products.Select(product => new SaleItem
                {
                    ProductId = product.ProductId,
                    Quantity = product.Quantity
                }).ToList();

                foreach (var saleItem in existingSale.Items)
                {
                    var product = await _productRepository.GetProductById(saleItem.ProductId);

                    if (saleItem.Quantity > 20)
                        throw new InvalidOperationException("You can only select up to 20 items");

                    saleItem.UnitPrice = product.Price;
                    saleItem.ProductName = product.Title;
                }
            }

            if (!existingSale.Items.Any() && sale.Items?.Any() == true)
            {
                existingSale.Items = sale.Items;
            }

            (decimal totalSaleAmount, decimal totalDiscount) = _discountCalculator.Calculate(sale.Items);

            existingSale.TotalSaleAmount = totalSaleAmount;
            existingSale.Discount = totalDiscount;
            existingSale.SaleDate = sale.SaleDate;
            existingSale.Items = sale.Items;

            var updatedSale = await _saleRepository.UpdateSale(existingSale);

            return _mapper.Map<UpdateSaleResult>(updatedSale);
        }
    }
}
