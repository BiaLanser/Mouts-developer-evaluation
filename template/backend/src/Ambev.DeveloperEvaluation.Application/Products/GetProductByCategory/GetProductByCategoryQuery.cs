using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProductByCategory
{
    public class GetProductByCategoryQuery : IRequest<GetProductByCategoryResult>
    {
        public string Category { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
        public ProductSortOrder Order { get; set; }
    }
}
