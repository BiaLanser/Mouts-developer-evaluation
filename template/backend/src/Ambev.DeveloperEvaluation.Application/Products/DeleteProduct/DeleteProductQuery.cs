using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct
{
    public class DeleteProductQuery : IRequest<DeleteProductResponse>
    {
        public int Id { get; set; }

        public DeleteProductQuery(int id)
        {
            Id = id;
        }
    }
}
