using Ambev.DeveloperEvaluation.Application.Products.GetProductByCategory;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductByCategory
{
    public class GetProductByCategoryProfile : Profile
    {
        public GetProductByCategoryProfile()
        {
            CreateMap<GetProductByCategoryRequest, GetProductByCategoryQuery>();
            CreateMap<GetProductByCategoryResult, GetProductByCategoryResponse>();
        }
    }
}
