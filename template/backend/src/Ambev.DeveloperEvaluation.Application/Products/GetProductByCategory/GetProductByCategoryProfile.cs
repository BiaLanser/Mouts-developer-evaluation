using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProductByCategory
{
    public class GetProductByCategoryProfile : Profile
    {
        public GetProductByCategoryProfile()
        {
            CreateMap<GetProductByCategoryQuery, GetProductByCategoryResult> ();
        }
    }
}
