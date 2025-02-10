using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProductByCategory
{
    public class GetProductByCategoryQueryValidator : AbstractValidator<GetProductByCategoryQuery>
    {
        public GetProductByCategoryQueryValidator() 
        {
            RuleFor(x => x.Category).NotEmpty();
        }
    }
}
