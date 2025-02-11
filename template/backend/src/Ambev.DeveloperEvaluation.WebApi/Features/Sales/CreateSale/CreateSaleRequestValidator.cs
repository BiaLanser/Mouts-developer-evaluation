using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
    {
        public CreateSaleRequestValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty().WithMessage("Invalid customer ID");
            RuleFor(x => x.Branch).NotEmpty().WithMessage("Branch cannot be empty");
            RuleFor(x => x.CartId).GreaterThan(0).WithMessage("Invalid Cart ID");
        }
    }
}
