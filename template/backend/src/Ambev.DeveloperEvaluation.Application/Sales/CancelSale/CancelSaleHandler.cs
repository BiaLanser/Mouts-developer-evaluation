using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{
    public class CancelSaleHandler : IRequestHandler<CancelSaleQuery, CancelSaleResult>
    {
        public readonly ISaleRepository _saleRepository;
        public readonly IMapper _mapper;

        public CancelSaleHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<CancelSaleResult> Handle(CancelSaleQuery request, CancellationToken cancellationToken)
        {
            var validator = new CancelSaleQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var sale = await _saleRepository.GetSaleBySaleNumber(request.SaleNumber);

            if (sale == null)
                throw new KeyNotFoundException("Sale not found.");

            sale.IsCancelled = true;
            sale.TotalSaleAmount = 0;  

            await _saleRepository.UpdateSale(sale);

            return _mapper.Map<CancelSaleResult>(sale);
        }
    }
}
