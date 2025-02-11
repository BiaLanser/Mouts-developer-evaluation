using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{
    public class CancelSaleQuery : IRequest<CancelSaleResult>
    {
        public int SaleNumber { get; set; }

        public CancelSaleQuery(int id)
        {
            SaleNumber = id;
        }
    }
}
