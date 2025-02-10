using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator() 
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Product ID is required");
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
