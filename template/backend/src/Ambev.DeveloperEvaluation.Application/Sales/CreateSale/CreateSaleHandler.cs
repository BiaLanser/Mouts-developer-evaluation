using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateSaleHandler> _logger;
        private readonly DiscountCalculator _discountCalculator;

        public CreateSaleHandler(ISaleRepository saleRepository, IProductRepository productRepository, IMapper mapper, ILogger<CreateSaleHandler> logger, DiscountCalculator discountCalculator)
        {
            _saleRepository = saleRepository;
            _productRepository = productRepository;
            _mapper = mapper;
            _logger = logger;
            _discountCalculator = discountCalculator;
        }

        public async Task<CreateSaleResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var validator = new CreateSaleCommandValidator();
                var validationResult = await validator.ValidateAsync(request, cancellationToken);

                if (!validationResult.IsValid)
                    throw new ValidationException(validationResult.Errors);


                var sale = _mapper.Map<Sale>(request);

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

                (totalSaleAmount, totalDiscount) = _discountCalculator.Calculate(sale.Items);

                sale.TotalSaleAmount = totalSaleAmount;
                sale.Discount = totalDiscount;
                sale.SaleDate = DateTime.UtcNow;
                sale.IsCancelled = false;

                var createdSale = await _saleRepository.AddSale(sale);
                return _mapper.Map<CreateSaleResult>(createdSale);
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error updating database");
                if (dbEx.InnerException != null)
                {
                    _logger.LogError(dbEx.InnerException, "Inner exception details:");
                }
                throw;

            }
        }
    }
}
