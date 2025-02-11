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
        private readonly IMapper _mapper;
        private readonly DiscountCalculator _discountCalculator;

        public UpdateSaleHandler(ISaleRepository saleRepository, IMapper mapper, DiscountCalculator discountCalculator)
        {
            _saleRepository = saleRepository;
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

            existingSale.SaleDate = sale.SaleDate;
            existingSale.Items = sale.Items;

            (decimal totalSaleAmount, decimal totalDiscount) = _discountCalculator.Calculate(sale.Items);

            existingSale.TotalSaleAmount = totalSaleAmount;
            existingSale.Discount = totalDiscount;

            var updatedSale = await _saleRepository.UpdateSale(existingSale);

            return _mapper.Map<UpdateSaleResult>(updatedSale);
        }
    }
}
