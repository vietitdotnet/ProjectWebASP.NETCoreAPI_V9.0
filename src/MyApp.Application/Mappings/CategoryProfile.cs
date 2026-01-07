using MyApp.Application.Models.DTOs.Categorys;
using MyApp.Application.Models.DTOs.Products;
using MyApp.Application.Models.Requests.Products;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Mappings
{
    public class CategoryProfile : BaseProfile
    {
    protected override void ConfigureResponsesMapping()
    {
        CreateMap<Category, CategoryDto>().ConstructUsing(src => new CategoryDto(src)).ReverseMap();

        CreateMap<Category, CategoryIdDto>();

    }
    protected override void ConfigureRequestsMapping()
    {
        
    }
}
}
