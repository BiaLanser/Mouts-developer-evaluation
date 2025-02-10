using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart
{
    public class CreateCartCommandValidator : AbstractValidator<CreateCartCommand>
    {
        public CreateCartCommandValidator()
        {
            RuleFor(cart => cart.UserId).GreaterThan(0).WithMessage("User ID must be greater than 0");

            RuleFor(cart => cart.Products)
                .NotEmpty().WithMessage("Cart must contain at least one product.")
                .Must(products => products.All(p => p.Quantity > 0)).WithMessage("All product quantities must be greater than 0.")
                .Must(products => products.All(p => p.ProductId > 0)).WithMessage("All product IDs must be greater than 0.");

            RuleForEach(cart => cart.Products).ChildRules(products =>
            {
                products.RuleFor(p => p.ProductId)
                    .NotEmpty().WithMessage("Product ID cannot be empty.")
                    .GreaterThan(0).WithMessage("Product ID must be greater than 0.");

                products.RuleFor(p => p.Quantity)
                    .GreaterThan(0).WithMessage("Product quantity must be greater than 0.");
            });
        }
    }
}
