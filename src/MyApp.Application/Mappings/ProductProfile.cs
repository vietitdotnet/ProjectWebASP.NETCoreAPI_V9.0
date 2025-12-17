
using AutoMapper;
using MyApp.Application.Models.DTOs.Products;
using MyApp.Application.Models.Requests.Products;
using MyApp.Domain.Entities;



namespace MyApp.Application.Mappings
{
    public class ProductProfile : BaseProfile
    {
        protected override void ConfigureResponsesMapping()
        {
            CreateMap<Product, ProductDto>().ConstructUsing(src => new ProductDto(src)).ReverseMap();
        }
        protected override void ConfigureRequestsMapping()
        {
            CreateMap<CreateProductRep, Product>();
            CreateMap<UpdateProductRep, Product>();
        }
    }
}
