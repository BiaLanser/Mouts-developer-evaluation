using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
    {
        public CreateSaleCommandValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty().WithMessage("Invalid customer ID");
            RuleFor(x => x.Branch).NotEmpty().WithMessage("Branch cannot be empty");
            RuleFor(x => x.CartId).GreaterThan(0).WithMessage("Invalid Cart ID");
        }
    }
}
