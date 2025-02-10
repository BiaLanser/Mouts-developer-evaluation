using Ambev.DeveloperEvaluation.Application.Products.GetProductByCategory;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetCategory
{
    public class GetProductByCategoryRequestValidator : AbstractValidator<GetProductByCategoryRequest>
    {
        public GetProductByCategoryRequestValidator()
        {
            RuleFor(x => x.Category).NotEmpty();
        }
    }
}
