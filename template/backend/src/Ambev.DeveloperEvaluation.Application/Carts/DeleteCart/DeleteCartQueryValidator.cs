using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.DeleteCart
{
    public class DeleteCartQueryValidator : AbstractValidator<DeleteCartQuery>
    {
        public DeleteCartQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Cart ID is required");
        }

    }
}
