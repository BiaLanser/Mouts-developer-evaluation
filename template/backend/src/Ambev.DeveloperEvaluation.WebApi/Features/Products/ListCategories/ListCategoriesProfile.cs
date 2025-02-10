using Ambev.DeveloperEvaluation.Application.Products.ListCategories;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ListCategory
{
    public class ListCategoriesProfile : Profile
    {
        public ListCategoriesProfile()
        {
            CreateMap<ListCategoriesResult, ListCategoriesResponse>();
        }
    }
}
