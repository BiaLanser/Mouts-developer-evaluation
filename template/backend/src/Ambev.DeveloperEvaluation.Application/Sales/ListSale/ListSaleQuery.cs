using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSale
{
    public class ListSaleQuery : IRequest<List<ListSaleResult>>;
}
