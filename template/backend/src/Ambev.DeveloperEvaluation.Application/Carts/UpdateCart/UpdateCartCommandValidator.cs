using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart
{
    public class UpdateCartCommandValidator : AbstractValidator<UpdateCartCommand>
    {
        public UpdateCartCommandValidator()
        {
            RuleFor(cart => cart.Id).GreaterThan(0);
            RuleFor(cart => cart.UserId).GreaterThan(0);
            RuleForEach(cart => cart.Products)
                .ChildRules(products =>
                {
                    products.RuleFor(p => p.ProductId).GreaterThan(0);
                    products.RuleFor(p => p.Quantity).GreaterThan(0);
                });
        }
    }
}

