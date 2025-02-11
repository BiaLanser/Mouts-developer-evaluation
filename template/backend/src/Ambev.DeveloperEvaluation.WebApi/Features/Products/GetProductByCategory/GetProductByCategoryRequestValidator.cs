using Ambev.DeveloperEvaluation.Application.Products.GetProductByCategory;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductByCategory
{
    public class GetProductByCategoryRequestValidator : AbstractValidator<GetProductByCategoryRequest>
    {
        public GetProductByCategoryRequestValidator()
        {
            RuleFor(x => x.Category).NotEmpty();
        }
    }
}
