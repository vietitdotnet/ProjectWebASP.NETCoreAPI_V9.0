using MyApp.Application.Models.DTOs.Products;

namespace MyApp.Application.Models.Responses.Products
{
    public class GetProductsRes
    {
       public IList<ProductDto> Data { get; set; }
    }
}
