using MyApp.Application.Core;
using MyApp.Application.Models.DTOs.Products;
using MyApp.Domain.Core;
using MyApp.Domain.Parameters;


namespace MyApp.Application.Models.Responses.Products
{
    public class GetProductsWithPagedRes
    {
        public PagedResponse<ProductDto, ProductParameters> Data { get; set; }
    }
}
