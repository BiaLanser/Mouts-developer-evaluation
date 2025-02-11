using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale
{
    public class DeleteSaleQuery : IRequest<DeleteSaleResponse>
    {
        public int SaleNumber { get; set; }

        public DeleteSaleQuery(int id)
        {
            SaleNumber = id;
        }
    }
}
