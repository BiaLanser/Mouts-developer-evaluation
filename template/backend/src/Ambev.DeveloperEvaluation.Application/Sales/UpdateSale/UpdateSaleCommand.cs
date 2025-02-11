using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleCommand : IRequest<UpdateSaleResult>
    {
        public int SaleNumber { get; set; }
        public int CustomerId { get; set; }
        public string Branch { get; set; }
        public int CartId { get; set; }
    }
}
