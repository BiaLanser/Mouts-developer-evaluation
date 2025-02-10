using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct
{
    public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
    {
        public UpdateProductRequestValidator()
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
