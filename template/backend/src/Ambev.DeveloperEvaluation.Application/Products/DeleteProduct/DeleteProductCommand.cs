using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct
{
    public class DeleteProductCommand : IRequest<DeleteProductResponse>
    {
        public int Id { get; set; }
    }
}
