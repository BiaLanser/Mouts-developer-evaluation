using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleProfile : Profile
    {
        public CreateSaleProfile()
        {
            CreateMap<CreateSaleCommand, Sale>()
                .ForMember(dest => dest.Items, opt => opt.Ignore());

            CreateMap<Sale, CreateSaleResult>()
                .ForMember(dest => dest.SaleItems, opt => opt.MapFrom(src => src.Items ?? new List<SaleItem>()));

            CreateMap<SaleItem, SaleItemDTO>();
        }
    }
}
