using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleCommandValidator : AbstractValidator<UpdateSaleCommand>
    {
        public UpdateSaleCommandValidator()
        {
            RuleFor(x => x.SaleNumber).GreaterThan(0).WithMessage("Sale number must be a positive integer.");
            RuleFor(x => x.CustomerId) .GreaterThan(0).WithMessage("Customer ID must be a positive integer.");
            RuleFor(x => x.Branch).NotEmpty().WithMessage("Branch must not be empty.").Length(3, 50).WithMessage("Branch name must be between 3 and 50 characters.");
            RuleFor(x => x.CartId).GreaterThan(0).WithMessage("Cart ID must be a positive integer.");
        }
    }
}
