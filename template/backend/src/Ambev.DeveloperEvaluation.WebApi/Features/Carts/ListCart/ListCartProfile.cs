using Ambev.DeveloperEvaluation.Application.Carts.ListCarts;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.ListCart
{
    public class ListCartProfile : Profile
    {
        public ListCartProfile()
        {
            CreateMap<Cart, CartResponse>();
            CreateMap<ListCartsResult, ListCartResponse>();
        }
    }
}
