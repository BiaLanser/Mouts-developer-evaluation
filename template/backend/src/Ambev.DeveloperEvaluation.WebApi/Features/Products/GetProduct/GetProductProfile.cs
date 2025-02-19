using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct
{
    public class GetProductProfile : Profile
    {
        public GetProductProfile()
        {
            CreateMap<int, GetProductQuery>()
            .ConstructUsing(id => new GetProductQuery(id));

            CreateMap<GetProductResult, GetProductResponse>();
        }
    }
}
