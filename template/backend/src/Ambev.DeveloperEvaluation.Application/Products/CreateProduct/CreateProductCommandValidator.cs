using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(product => product.Title).NotEmpty().Length(3, 100);
            RuleFor(product => product.Price).GreaterThan(0);
            RuleFor(product => product.Description).NotEmpty().Length(5, 500);
            RuleFor(product => product.Category).NotEmpty();
            RuleFor(product => product.Image).NotEmpty();
            RuleFor(product => product.Rating.Count).GreaterThanOrEqualTo(0);
            RuleFor(product => product.Rating.Rate).InclusiveBetween(0, 5);
        }
    }
}
