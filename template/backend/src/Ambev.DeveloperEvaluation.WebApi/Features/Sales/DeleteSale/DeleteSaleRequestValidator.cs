using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale
{
    public class DeleteSaleRequestValidator : AbstractValidator<DeleteSaleRequest>
    {
        public DeleteSaleRequestValidator()
        {
            RuleFor(x => x.SaleNumber).NotEmpty().WithMessage("Sale Number is required");
        }
    }
}
