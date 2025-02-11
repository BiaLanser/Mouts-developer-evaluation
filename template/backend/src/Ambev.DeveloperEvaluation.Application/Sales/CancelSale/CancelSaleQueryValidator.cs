using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{
    public class CancelSaleQueryValidator : AbstractValidator<CancelSaleQuery>
    {
        public CancelSaleQueryValidator()
        {
            RuleFor(x => x.SaleNumber).NotEmpty().WithMessage("Sale Number is required");
        }
    }
}
