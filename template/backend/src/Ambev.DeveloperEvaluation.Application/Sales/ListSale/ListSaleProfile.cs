using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSale
{
    public class ListSaleProfile : Profile
    {
        public ListSaleProfile()
        {
            CreateMap<Sale, ListSaleResult>()
                 .ForMember(dest => dest.SaleItems, opt => opt.MapFrom(src => src.Items));

            CreateMap<SaleItem, SaleItemDTO>();
        }
    }
}
