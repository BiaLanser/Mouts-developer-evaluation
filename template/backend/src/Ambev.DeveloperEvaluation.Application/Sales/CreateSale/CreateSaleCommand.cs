using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleCommand : IRequest<CreateSaleResult>
    {
        public int CustomerId { get; set; }
        public string Branch { get; set; }
        public int CartId { get; set; }
    }
}
