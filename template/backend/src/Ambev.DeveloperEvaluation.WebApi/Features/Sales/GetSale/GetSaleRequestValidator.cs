using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale
{
    public class GetSaleRequestValidator : AbstractValidator<GetSaleRequest>
    {
        public GetSaleRequestValidator() 
        {
            RuleFor(x => x.SaleNumber).NotEmpty().WithMessage("Sale Number is required");
        }
    }
}
