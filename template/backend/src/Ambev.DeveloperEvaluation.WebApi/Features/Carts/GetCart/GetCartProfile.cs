﻿using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCart
{
    public class GetCartProfile : Profile
    {
        public GetCartProfile()
        {
            CreateMap<int, GetCartQuery>()
            .ConstructUsing(id => new GetCartQuery(id));

            CreateMap<GetCartResult, GetCartResponse>();
        }
    }
}
