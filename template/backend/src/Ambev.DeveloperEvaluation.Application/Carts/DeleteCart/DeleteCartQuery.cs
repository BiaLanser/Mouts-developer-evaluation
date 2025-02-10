using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.DeleteCart
{
    public class DeleteCartQuery : IRequest<DeleteCartResponse>
    {
        public int Id { get; set; }

        public DeleteCartQuery(int id)
        {
            Id = id;
        }
    }
}
