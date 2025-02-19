using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.Products.ListProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ListProducts
{
    public class ListProductProfile : Profile
    {
        public ListProductProfile()
        {
            CreateMap<Product, ProductResponse>();
            CreateMap<ListProductsResult, ListProductResponse>();
        }
    }
}
