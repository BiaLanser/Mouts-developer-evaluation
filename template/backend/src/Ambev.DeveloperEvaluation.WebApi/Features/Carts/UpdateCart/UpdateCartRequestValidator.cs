using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart
{
    public class UpdateCartRequestValidator : AbstractValidator<UpdateCartRequest>
    {
        public UpdateCartRequestValidator()
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
