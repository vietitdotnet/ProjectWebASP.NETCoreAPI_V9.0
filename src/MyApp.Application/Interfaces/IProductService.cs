using MyApp.Application.Models.Requests.Products;
using MyApp.Application.Models.Responses.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Interfaces
{
    public interface IProductService
    {
        Task<GetProductsRes> GetProductsAsync();
        Task<GetProductRes> GetProductByIdAsync(int id);
        Task<CreateProductRes> CreateProductAsync(CreateProductRep req);
        Task<DeleteProductRes> DeleteProductAsync(int id);

        Task<UpdateProductRes> UpdateProductAsync(int id, UpdateProductRep req);
    }
}
