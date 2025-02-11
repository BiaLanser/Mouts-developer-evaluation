using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    public class GetSaleQueryValidator : AbstractValidator<GetSaleQuery>
    {
        public GetSaleQueryValidator()
        {
            RuleFor(x => x.SaleNumber).NotEmpty().WithMessage("Sale Number is required");
        }
    }

}
