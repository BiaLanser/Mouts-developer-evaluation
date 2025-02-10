using Ambev.DeveloperEvaluation.Application.Products.ListProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ListProducts
{
    public class ListProductProfile : Profile
    {
        public ListProductProfile() 
        {
            CreateMap<ListProductResult, ListProductResponse>();

        }
}
