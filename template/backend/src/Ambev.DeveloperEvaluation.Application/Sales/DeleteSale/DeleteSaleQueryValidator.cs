using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale
{
    public class DeleteSaleQueryValidator : AbstractValidator<DeleteSaleQuery>
    {
        public DeleteSaleQueryValidator()
        {
            RuleFor(x => x.SaleNumber).NotEmpty().WithMessage("Sale Number is required");
        }
    }
}
