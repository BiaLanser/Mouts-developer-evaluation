using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart
{
    public class GetCartQuery : IRequest<GetCartResult>
    {
        public int Id { get; set; }

        public GetCartQuery(int id)
        {
            Id = id;
        }
    }
}
