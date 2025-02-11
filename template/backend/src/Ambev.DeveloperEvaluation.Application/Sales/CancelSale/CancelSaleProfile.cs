using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{
    public class CancelSaleProfile : Profile
    {
        public CancelSaleProfile()
        {
            CreateMap<CancelSaleQuery, Sale>();
            CreateMap<Sale, CancelSaleResult>();
        }
    }
}
