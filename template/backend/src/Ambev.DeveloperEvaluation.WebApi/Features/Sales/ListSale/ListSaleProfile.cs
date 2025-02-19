using Ambev.DeveloperEvaluation.Application.Sales;
using Ambev.DeveloperEvaluation.Application.Sales.ListSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSale
{
    public class ListSaleProfile : Profile
    {
        public ListSaleProfile()
        {
            CreateMap<ListSaleResult, ListSaleResponse>()
                .ForMember(dest => dest.SaleItems, opt => opt.MapFrom(src => src.SaleItems));

            CreateMap<SaleItem, SaleItemDTO>();
        }
    }
}
