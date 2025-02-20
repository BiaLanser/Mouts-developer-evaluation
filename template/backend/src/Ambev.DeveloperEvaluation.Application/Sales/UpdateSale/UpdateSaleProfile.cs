using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleProfile : Profile
    {
        public UpdateSaleProfile()
        {
            CreateMap<UpdateSaleCommand, Sale>();
              //  .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

            CreateMap<Sale, UpdateSaleResult>()
                 .ForMember(dest => dest.SaleItems, opt => opt.MapFrom(src => src.Items ?? new List<SaleItem>()));

            CreateMap<SaleItem, SaleItemDTO>();
        }
    }
}
