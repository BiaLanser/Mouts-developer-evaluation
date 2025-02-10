using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProduct
{
    public class ListProductsQuery : IRequest<ListProductsResult>
    {
        public int Page { get; set; }
        public int Size { get; set; }
        public ProductSortOrder Order { get; set; }
    }
}
