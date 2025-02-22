﻿using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    public class GetSaleProfile : Profile
    {
        public GetSaleProfile() 
        {
            CreateMap<Sale, GetSaleResult>()
                .ForMember(dest => dest.SaleItems, opt => opt.MapFrom(src => src.Items));

            CreateMap<SaleItem, SaleItemDTO>();
        }
    }
}
