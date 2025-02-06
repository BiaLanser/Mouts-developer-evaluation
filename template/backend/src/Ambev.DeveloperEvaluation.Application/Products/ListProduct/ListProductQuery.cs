using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProduct
{
    public class ListProductQuery : IRequest<ListProductResult>
    {
        public int Page { get; set; }
        public int Size { get; set; }
        public ProductSortOrder Order { get; set; }
    }
}
