using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale
{
    public class DeleteSaleHandler : IRequestHandler<DeleteSaleQuery, DeleteSaleResponse>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly ILogger<DeleteSaleHandler> _logger;

        public DeleteSaleHandler(ISaleRepository saleRepository, ILogger<DeleteSaleHandler> logger)
        {
            _saleRepository = saleRepository;
            _logger = logger;
        }

        public async Task<DeleteSaleResponse> Handle(DeleteSaleQuery request, CancellationToken cancellationToken)
        {
            var validator = new DeleteSaleQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var sale = await _saleRepository.GetSaleBySaleNumber(request.SaleNumber);
            if (sale == null)
            {
                return new DeleteSaleResponse { Success = false };
            }

            await _saleRepository.DeleteSale(request.SaleNumber);
            return new DeleteSaleResponse { Success = true };
        }
    }
}
